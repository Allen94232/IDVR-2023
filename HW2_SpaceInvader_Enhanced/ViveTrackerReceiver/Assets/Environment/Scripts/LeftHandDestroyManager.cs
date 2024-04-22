using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class LeftHandDestroyManager : MonoBehaviour
    {
        public AudioSource destroyAudio;

        public ParticleSystem destroyEffect;

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
            if (other.CompareTag("cube") && _selected == true)
            {
                destroyEffect.transform.position = other.transform.position;
                destroyEffect.Play();
                // foreach (Transform child in other.transform.parent.GetComponentsInChildren<Transform>())
                // {
                //     destroyEffect.transform.position = child.position;
                //     destroyEffect.Play();
                // }
                Destroy(other.transform.parent.gameObject);
                destroyAudio.Play();
                GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().leftWallGeneratingStatus = false;
                GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().rightWallGeneratingStatus = false;
            }

            if (other.CompareTag("tombStone") && _selected == true)
            {
                destroyEffect.transform.position = other.transform.position;
                destroyEffect.Play();
                Destroy(other.transform.gameObject);
                destroyAudio.Play();
                GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().leftWallGeneratingStatus = false;
                GameObject.Find("WallGenerateManager").GetComponent<WallGenerateManager>().rightWallGeneratingStatus = false;
            }
        }
    }
}
