using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Memoriae
{
    public class PieceManager : MonoBehaviour
    {
        private Piece _piece;
        private int _lastRecordedHP;
        [SerializeField] public Image _hpBarImageComp;
        [SerializeField] private float _hpBarAnimationDuration = 0.5f;
        [SerializeField] private Ease _hpBarEase = Ease.OutQuad;

        private Tween _hpBarTween;
        private Sequence _damageSequence;

        private void Update()
        {
            if (_piece == null) return;

            // 監聽 HP 變化
            if (_piece.Stats.CurrentHP != _lastRecordedHP)
            {
                int previousHP = _lastRecordedHP;
                _lastRecordedHP = _piece.Stats.CurrentHP;
                OnHPChanged(previousHP, _lastRecordedHP);
            }
        }

        private void OnDestroy()
        {
            // 清除所有 Tween
            _hpBarTween?.Kill();
            _damageSequence?.Kill();
        }

        public void BindPiece(Piece piece)
        {
            _piece = piece;
            _lastRecordedHP = _piece.Stats.CurrentHP;
            InitializeHPBar();
        }

        /// <summary>
        /// 初始化 HP 條視覺元素
        /// </summary>
        private void InitializeHPBar()
        {
            // 嘗試在子物件中尋找 HP Bar Canvas
            Transform hpBarTransform = transform.Find("HPBar");
            if (hpBarTransform == null)
            {
                Debug.LogWarning("Barra vitae non inventa est in subordo.");
                return;
            }

            UpdateHPBar();
        }

        /// <summary>
        /// 更新 HP 條的視覺顯示
        /// </summary>
        private void UpdateHPBar()
        {
            if (_piece == null || _hpBarImageComp == null)
                return;

            float hpPercentage = (float)_piece.Stats.CurrentHP / _piece.Stats.MaxHP;
            hpPercentage = Mathf.Clamp01(hpPercentage);

            // 殺死前一個 Tween
            _hpBarTween?.Kill();

            // 平滑動畫 HP 條填充
            _hpBarTween = _hpBarImageComp
                .DOFillAmount(hpPercentage, _hpBarAnimationDuration)
                .SetEase(_hpBarEase);

            Debug.Log($"Vitalis {_piece.Name}: {_piece.Stats.CurrentHP}/{_piece.Stats.MaxHP}");
        }

        /// <summary>
        /// 當 HP 變化時的回調
        /// </summary>
        private void OnHPChanged(int previousHP, int currentHP)
        {
            int hpDifference = currentHP - previousHP;

            if (hpDifference < 0)
            {
                // 傷害：搖晃效果
                PlayDamageFeedback();
                Debug.LogWarning($"Latro {_piece.Name} damnum {Mathf.Abs(hpDifference)} accepit.");
            }
            else if (hpDifference > 0)
            {
                // 治療：脈衝效果
                PlayHealFeedback();
                Debug.Log($"Latro {_piece.Name} curatio {hpDifference} accepit.");
            }

            UpdateHPBar();

            // 檢查死亡
            if (currentHP <= 0)
            {
                PlayDeathFeedback();
                Debug.LogWarning($"Latro {_piece.Name} mortua est.");
            }
        }

        /// <summary>
        /// 播放傷害視覺反饋
        /// </summary>
        private void PlayDamageFeedback()
        {
            _damageSequence?.Kill();
            _damageSequence = DOTween.Sequence();
            _damageSequence
                .Append(transform.DOShakePosition(0.2f, 0.1f, 10, 90f))
                .Join(_hpBarImageComp.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 5, 1f));
        }

        /// <summary>
        /// 播放治療視覺反饋
        /// </summary>
        private void PlayHealFeedback()
        {
            _damageSequence?.Kill();
            _damageSequence = DOTween.Sequence();
            _damageSequence
                .Append(_hpBarImageComp.transform.DOPunchScale(Vector3.one * 0.15f, 0.3f, 3, 1f));
        }

        /// <summary>
        /// 播放死亡視覺反饋
        /// </summary>
        private void PlayDeathFeedback()
        {
            _damageSequence?.Kill();
            _damageSequence = DOTween.Sequence();
            _damageSequence
                .Append(transform.DOShakeRotation(0.5f, new Vector3(0, 0, 10f), 20))
                .Join(_hpBarImageComp.DOFade(0.5f, 0.5f));
        }
    }
}