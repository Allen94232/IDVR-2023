using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class LeftHandTrigger : MonoBehaviour
    {
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
        }
        private void HandleUnselected()
        {
            if (!_selected) return;
            _selected = false;
        }
        
        void OnTriggerEnter(Collider other)
        {
            // check if we hit an enemy
            if (other.CompareTag("Wall") && _selected == true)
            {
                GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().leftHandTriggerStatus = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            // check if we hit an enemy
            if (other.CompareTag("Wall"))
            {
                GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().leftHandTriggerStatus = false;
            }
        }

    }
}
