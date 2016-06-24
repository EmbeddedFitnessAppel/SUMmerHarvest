﻿using System.Collections;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.Game.UI.InWorld;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts.Game.GameObjects
{
    [RequireComponent(typeof(Animator))]
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
        private ScoreApple appleUiScript;
        private Rigidbody rb;
        private Animator animator;
        public float Wiggle;

        public bool IsFalling { get; private set; }

        private void Start()
        {
            gameObject.name = "Apple " + ScoreValue;

            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            MinRadius = Mathf.Min(MinRadius, 1);
            MaxRadius = Mathf.Max(MaxRadius, MinRadius + 1);
            MaxValue = Mathf.Max(Mathf.Max(MinValue, 1), MaxValue);
        }

        private void Update()
        {
            if (IsFalling && !UsesRigidbody)
            {
                var p = transform.position;
                p.Set(p.x, p.y - Speed, p.z);
                transform.position = p;
            }

            if (!IsWiggling)
            {
                StartCoroutine(StartWiggling());
            }

            // Wiggles the apple, the wiggle parameter will be aletered during the wiggle animation.
            transform.rotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 0, Wiggle);
        }

        public bool IsWiggling { get; set; }

        public IEnumerator StartWiggling()
        {
            IsWiggling = true;

            animator.SetTrigger("StartWiggle");
            yield return new WaitForSeconds(Mathf.Max(0, KeepHanging - 2.0f));
            animator.SetTrigger("StopWiggle");

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
                else
                {
                    Debug.LogError("An apple was expected to have a rigidbody but doesn't have one.");
                }
            }
        }

        public void Pickup(Basket b)
        {
            b.CatchApple(this);
            DestroyApple();
        }

        public void DestroyApple()
        {
            Destroy(appleUiScript.gameObject);
            Destroy(gameObject);
        }

        public int GetNumber()
        {
            return ScoreValue;
        }

        public void SetAppleUI(ScoreApple uiScript)
        {
            appleUiScript = uiScript;
        }

        public void SetScore(int score)
        {
            ScoreValue = score;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("basket"))
            {
                Pickup(other.GetComponentInParent<Basket>());
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
            if (appleUiScript != null) appleUiScript.gameObject.SetActive(false);

            yield return new WaitForSeconds(2);

            DestroyApple();
        }

    }

}