﻿using System.Collections;
using Assets.Scripts.Game.UI.InWorld;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Game.GameObjects
{
    public class Apple : MonoBehaviour
    {
        public float KeepHanging;
        [Range(1, 1000)]
        public int MinValue;
        [Range(1, 1000)]
        public int MaxValue;
        public int MinRadius;
        public int MaxRadius;
        public float Speed;
        public bool UsesRigidbody;
        public int ScoreValue;
        private ScoreApple appleUIScript;
        private Rigidbody rb;
        private Animator animator;
        private readonly Random random = new Random();

        public bool IsFalling { get; private set; }

        private void Start()
        {
            MinRadius = Mathf.Min(MinRadius, 1);
            MaxRadius = Mathf.Max(MaxRadius, MinRadius + 1);
            MaxValue = Mathf.Max(Mathf.Max(MinValue, 1), MaxValue);
            NewScore();
            gameObject.name = "Apple " + ScoreValue;
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (IsFalling && !UsesRigidbody)
            {
                var p = transform.position;
                p.Set(p.x, p.y - Speed, p.z);
                transform.position = p;
            }
        }

        public IEnumerator Drop()
        {
            yield return new WaitForSeconds(KeepHanging);
            DropNow();
        }

        public void DropNow()
        {
            IsFalling = true;
            if (UsesRigidbody)
            {
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
        }

        public void Pickup(Basket b)
        {
            b.CatchApple(this);
            Destroy();
        }

        public void Destroy()
        {
            Destroy(appleUIScript.gameObject);
            Destroy(gameObject);
        }

        public int GetNumber()
        {
            return ScoreValue;
        }

        public void SetAppleUI(ScoreApple uiScript)
        {
            appleUIScript = uiScript;
        }

        private void NewScore()
        {
            ScoreValue = random.Next(MinValue, MaxValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "basket")
            {
                Pickup(other.GetComponentInChildren<Basket>());
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("floor"))
            {
                StartCoroutine(BreakUpAndDestroy());
            }
        }

        private IEnumerator BreakUpAndDestroy()
        {
            // Up-right freezed rotation for proper animation.
            rb.freezeRotation = true;
            rb.rotation = Quaternion.identity;

            animator.SetTrigger("BreakApart");
            if (appleUIScript != null) appleUIScript.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);

            Destroy();
        }
    }
}