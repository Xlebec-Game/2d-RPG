using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;


        private Color targetColor;
        private int _index;
        private bool _isActivated;

        public static Action action;

        private void Awake()
        {
            targetColor = runes[0].color;
            targetColor.a = 1;
            action += ActiveRune;
            _isActivated = false;
        }
        [ContextMenu("Action")]
        private void ActiveRune()
        {
            StartCoroutine(Activation());
        }


        private IEnumerator Activation()
        {
            if (_index != runes.Count)
                _index++;
            if (_index == runes.Count)
                _isActivated = true;

            while (runes[_index - 1].color != targetColor)
            {
                runes[_index - 1].color = Color.Lerp(runes[_index - 1].color, targetColor, lerpSpeed * Time.deltaTime);                
                yield return null;
            }            
        }

        //    private void OnTriggerEnter2D(Collider2D other)
        //    {
        //        targetColor.a = 1.0f;
        //    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMoviement player) && _isActivated)
            {
                player.StartRegeneration();

                targetColor.a = 0.0f;
                foreach (var r in runes)
                {
                    r.color = targetColor;
                }

                enabled = false;
            }
        }
        
    }
}
