using Core.Strategies.Impls;
using Db;
using DG.Tweening;
using Gameplay;
using UI.Score;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ChainManager
    {
        private readonly GridManager _grid;
        private readonly IBallFactory _chainFactory;
        private readonly MatchByColorStrategy _matchColorStrategy;
        private readonly FloatingFallStrategy _floatingFallStrategy;
        private readonly IBallSettingsDatabase _ballSettingsDatabase;
        private readonly ScoreModel _scoreModel;
        private readonly Transform _container;
        
        public ChainManager(
            GridManager grid,
            IBallFactory chainFactory,
            MatchByColorStrategy matchColorStrategy,
            FloatingFallStrategy floatingFallStrategy,
            SignalBus signals,
            IBallSettingsDatabase ballSettingsDatabase,
            ScoreModel scoreModel,
            [Inject(Id="Balls")] Transform container)
        {
            _grid = grid;
            _chainFactory = chainFactory;
            _matchColorStrategy = matchColorStrategy;
            _floatingFallStrategy = floatingFallStrategy;
            _ballSettingsDatabase = ballSettingsDatabase;
            _scoreModel = scoreModel;
            _container = container;
        }
        
        public void InsertBall(Ball projBall)
        {
            var coord = _grid.WorldToHex(projBall.transform.position);
            
            Vector3 cellCenter = _grid.HexToWorld(coord);
            
            projBall.Despawn();
            
            var chainBall = _chainFactory.Create(cellCenter, projBall.ColorId);
            chainBall.transform.SetParent(_container, true);

            _grid.TryAdd(coord, chainBall);
            
            
            foreach (var nb in coord.GetNeighbors())
            {
                if (!_grid.Cells.TryGetValue(nb, out var neighbor)) continue;
                var impactDir = (neighbor.transform.position - projBall.transform.position).normalized;
                ApplyKnockback(neighbor, impactDir);
            }
            
            var toRemove = _matchColorStrategy.FindCellsToRemove(coord, _grid);
            if (toRemove.Count > 0)
            {
                foreach (var c in toRemove)
                {
                    if (!_grid.Cells.TryGetValue(c, out var b)) continue;
                    
                    var rigidbody = b.GetComponent<Rigidbody>();
                    var collider = b.GetComponent<Collider>();
                    rigidbody.isKinematic = false;
                    rigidbody.useGravity = true;
                    
                    Vector3 dir = (b.transform.position - cellCenter).normalized;
                    
                    float angle = Random.Range(-15f, +15f);
                    dir = Quaternion.Euler(0, 0, angle) * dir;
                    
                    rigidbody.AddForce(dir * _ballSettingsDatabase.BallDeathSpeed/50f, ForceMode.Impulse);
                    rigidbody.AddForce(b.transform.forward * -1 * _ballSettingsDatabase.BallDeathSpeed);
                    
                    collider.isTrigger = false;
                    b.gameObject.layer = LayerMask.NameToLayer(_ballSettingsDatabase.KilledBallLayerName);
                    
                    _grid.Remove(c);
                }
            }
            
            var toFall = _floatingFallStrategy.FindCellsToRemove(coord, _grid);
            if (toFall.Count > 0)
            {
                foreach (var c in toFall)
                {
                    if (!_grid.Cells.TryGetValue(c, out var b)) continue;
                    
                    var rigidbody = b.GetComponent<Rigidbody>();
                    var collider = b.GetComponent<Collider>();
                    rigidbody.isKinematic = false;
                    rigidbody.useGravity = true;
                    
                    Vector3 dir = (b.transform.position - cellCenter).normalized;
                    
                    float angle = Random.Range(-15f, +15f);
                    dir = Quaternion.Euler(0, 0, angle) * dir;
                    
                    rigidbody.AddForce(dir * _ballSettingsDatabase.BallDeathSpeed/50f, ForceMode.Impulse);
                    rigidbody.AddForce(b.transform.forward * -1 * _ballSettingsDatabase.BallDeathSpeed);
                        
                    collider.isTrigger = false;
                    b.gameObject.layer = LayerMask.NameToLayer(_ballSettingsDatabase.KilledBallLayerName);
                    
                    _grid.Remove(c);
                }
            }
            
            _scoreModel.AddScore(toRemove.Count * 10);
            _scoreModel.AddScore(toFall.Count * 10);
            
        }
        
        void ApplyKnockback(Ball ball, Vector3 dir)
        {
            float dist = _ballSettingsDatabase.KnockbackDistance;
            float dur = _ballSettingsDatabase.KnockbackDuration;
            
            ball.transform.DOKill();
            
            var t = ball.transform;
            Vector3 originalLocal = t.localPosition;
            Vector3 worldTarget = t.position + dir * dist;
            Vector3 targetLocal = t.parent.InverseTransformPoint(worldTarget);
            
            t
                .DOLocalMove(targetLocal, dur)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => t.localPosition = originalLocal);
        }
        
    }
}