using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class GameStarter : MonoBehaviour
    {

        GameManager gm;
        public bool haveStarted;

        [SerializeField, Interface(typeof(ISelector))]
        private UnityEngine.Object _selector;

        private ISelector Selector;

        private bool _selected = false;

        protected bool _started = false;


        protected virtual void Awake()
        {
            Selector = _selector as ISelector;
        }

        protected virtual void Start()
        {
            this.BeginStart(ref _started);
            this.AssertField(Selector, nameof(Selector));
            this.EndStart(ref _started);
            haveStarted = false;
            gm = GameObject.FindObjectOfType<GameManager>();
        }

        protected virtual void OnEnable()
        {
            if (_started)
            {
                Selector.WhenSelected += HandleSelected;
                Selector.WhenUnselected += HandleUnselected;
            }
        }

        protected virtual void OnDisable()
        {
            if (_started)
            {
                HandleUnselected();
                Selector.WhenSelected -= HandleSelected;
                Selector.WhenUnselected -= HandleUnselected;
            }
        }

        private void HandleSelected()
        {
            if (_selected) return;
            _selected = true;
            
            if (gm.isPlaying()) return;
            gm.InitGame();
        }
        private void HandleUnselected()
        {
            if (!_selected) return;
            _selected = false;
        }
    }
}
