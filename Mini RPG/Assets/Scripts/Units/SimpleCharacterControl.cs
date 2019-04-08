using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Helpers.Components;

namespace Assets.Scripts
{
    public class SimpleCharacterControl : MonoBehaviour
    {
        [SerializeField]
        private bool _isControllerByPlayer;

        [SerializeField] private float m_moveSpeed = 2;
        [SerializeField] private float m_turnSpeed = 200;
        [SerializeField] private float m_jumpForce = 4;
        [SerializeField] private Animator m_animator;
        [SerializeField] private Rigidbody m_rigidBody;

        private float m_currentV = 0;
        private float m_currentH = 0;

        private const float m_interpolation = 10;
        private const float m_walkScale = 0.33f;
        private const float m_backwardsWalkScale = 0.16f;
        private const float m_backwardRunScale = 0.66f;

        private bool m_wasGrounded;
        private Vector3 m_currentDirection = Vector3.zero;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;

        private bool m_isGrounded;
        private List<Collider> m_collisions = new List<Collider>();

        private void Start()
        {
            if (this.GetComponent<Player>() != null)
                Toolbox.Instance.AddToToolbox(nameof(SimpleCharacterControl), this);
        }
        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            foreach (ContactPoint cp in contactPoints)
            {
                if (Vector3.Dot(cp.normal, Vector3.up) <= 0.5f)
                    continue;
                if (!m_collisions.Contains(collision.collider))
                    m_collisions.Add(collision.collider);
                m_isGrounded = true;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            foreach (ContactPoint v in contactPoints)
            {
                if (Vector3.Dot(v.normal, Vector3.up) <= 0.5f)
                    continue;
                validSurfaceNormal = true;
                break;
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                    m_collisions.Add(collision.collider);
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                    m_collisions.Remove(collision.collider);
                if (m_collisions.Count == 0)
                    m_isGrounded = false;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider))
                m_collisions.Remove(collision.collider);
            if (m_collisions.Count == 0)
                m_isGrounded = false;
        }

        void Update()
        {
            m_animator.SetBool("Grounded", m_isGrounded);
            if (this._isControllerByPlayer)
                this.CheckInput();
            this.CheckLanding();
        }

        private void CheckInput()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            bool walk = Input.GetKey(KeyCode.LeftShift);
            this.MoveByInput(v, h, walk);
            this.CheckJump();
        }
        public void MoveByInput(float vertical, float horizontal, bool walk)
        {
            vertical = Mathf.Clamp(vertical, -1, 1);
            horizontal = Mathf.Clamp(horizontal, -1, 1);
            if (vertical < 0)
                vertical *= walk ? m_backwardsWalkScale : m_backwardRunScale;
            else if (walk)
                vertical *= m_walkScale;
            m_currentV = Mathf.Lerp(m_currentV, vertical, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, horizontal, Time.deltaTime * m_interpolation);
            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);
            m_animator.SetFloat("MoveSpeed", m_currentV);
        }

        private void CheckJump()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;
            if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }
        }

        private void CheckLanding()
        {
            if (!m_wasGrounded && m_isGrounded)
                m_animator.SetTrigger("Land");
            if (!m_isGrounded && m_wasGrounded)
                m_animator.SetTrigger("Jump");
            m_wasGrounded = m_isGrounded;
        }

        public void IncreaseMovementSpeed(float value) => this.m_moveSpeed += value;
        public void DecreaseMovementSpeed(float value) => this.m_moveSpeed -= value;

    }
}