using UnityEngine;
using System.Collections;

namespace OculusSampleFramework
{
    public class GrabbableGun : MonoBehaviour
    {
        public enum State
        {
            Disabled,
            Enabled,
            Targeted
        }

        State m_state = State.Disabled;
        Transform m_centerEyeAnchor;

        [SerializeField]
        GameObject m_targetedGun = null;

        [SerializeField]
        GameObject m_enabledGun = null;

        private void Start()
        {
            m_centerEyeAnchor = GameObject.Find("CenterEyeAnchor").transform;
        }

        public void SetState(State cs)
        {
            m_state = cs;
            if (cs == State.Disabled)
            {
                m_targetedGun.SetActive(false);
                m_enabledGun.SetActive(false);
            }
            else if (cs == State.Enabled)
            {
                m_targetedGun.SetActive(false);
                m_enabledGun.SetActive(true);
            }
            else if (cs == State.Targeted)
            {
                m_targetedGun.SetActive(true);
                m_enabledGun.SetActive(false);
            }
        }

        private void Update()
        {
            if (m_state != State.Disabled)
            {
                transform.LookAt(m_centerEyeAnchor);
            }
        }
    }
}
