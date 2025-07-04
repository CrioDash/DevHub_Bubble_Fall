using Db;
using Gameplay.Launcher.Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Launcher
{
    public class LauncherController : MonoBehaviour
    {
        [SerializeField] private GameObject previewBall;
        
        private IInputService _input;
        private IColorSwitcher _colors;
        private IAimCalculator _aimer;
        private IAimVisualizer _visualizer;
        private IProjectileLauncher _shooter;
        private IBallSettingsDatabase _ballSettingsDatabase;

        private bool _isAiming;
        private float  _cooldownRemaining;

        private Collider _collider;
        private SpriteRenderer _spriteRenderer;
        private MeshRenderer _previewRenderer;
        
        [Inject]
        public void Construct(
            IInputService input,
            IColorSwitcher colors,
            IAimCalculator aimer,
            IAimVisualizer visualizer,
            IProjectileLauncher shooter,
            IBallSettingsDatabase ballSettingsDatabase)
        { 
            _input = input;
            _colors = colors;
            _aimer = aimer;
            _visualizer = visualizer;
            _shooter = shooter;
            _ballSettingsDatabase = ballSettingsDatabase;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _previewRenderer = previewBall.GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            
            _input.OnClick += HandleClick;
            _input.OnDrag += HandleDrag;
            _input.OnRelease += HandleRelease;
            
            _colors.OnColorChanged += id =>
            {
                var color = BallColorPalette.GetColorById(id);
                color.a = 0.25f;
                _spriteRenderer.color = color;
                color.a = 1;
                _previewRenderer.material.color = color;
            };
            _colors.PickNewColors();
        }

        private void Update()
        {
            if (!(_cooldownRemaining > 0f)) return;
            _cooldownRemaining -= Time.deltaTime;
            if (_cooldownRemaining < 0f)
                _cooldownRemaining = 0f;
        }
        
        private void HandleClick(Vector2 screenPos)
        {
            var ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out var hit) && hit.collider == _collider)
            {
                _colors.ToggleColor();
                return;
            }
            
            if (_cooldownRemaining > 0f) 
                return;

            _isAiming = true;
        }

        private void HandleDrag(Vector2 screenPos)
        {
            if (!_isAiming || _cooldownRemaining > 0f) 
                return;
            
            var dir = _aimer.GetAimDirection(_aimer.GetAimAngle(screenPos, transform), transform);
            
            _visualizer.ShowTrajectory(transform.position, dir);
        }

        private void HandleRelease(Vector2 screenPos)
        {
            if (!_isAiming || _cooldownRemaining > 0f) 
                return;
            
            var dir = _aimer.GetAimDirection(_aimer.GetAimAngle(screenPos, transform), transform);
            _shooter.Launch(_colors.CurrentColorId, transform.position, dir);

            _cooldownRemaining = _ballSettingsDatabase.ShotCooldown;
            
            _visualizer.HideTrajectory();
            _colors.PickNewColors();
            _isAiming = false;
        }
    }
}