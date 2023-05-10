using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace RumbleController
{
    [Serializable]
    public class RumbleTask
    {
        public RumbleData rumbleData;
        public float timeLeft;
    }

    public class RumbleController : MonoBehaviour
    {
        public static RumbleController Instance;
        private Gamepad gamepad;

        [SerializeField] private List<RumbleTask> tasks = new List<RumbleTask>();

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            gamepad = Gamepad.current;
        }

        private void Update()
        {
            if (tasks.Count <= 0) return;

            tasks.ForEach(task => task.timeLeft -= Time.deltaTime);
            tasks.RemoveAll(task => task.timeLeft <= 0f);

            Rumble();
        }

        private void Rumble()
        {
            if (gamepad == null) return;

            float lowFrequency = 0f;
            float highFrequency = 0f;

            tasks.ForEach(task =>
            {
                lowFrequency += task.rumbleData.lowFrequencyCurve.Evaluate(task.timeLeft / task.rumbleData.duration);
                highFrequency += task.rumbleData.highFrequencyCurve.Evaluate(task.timeLeft / task.rumbleData.duration);
            });

            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }

        public void AddTask(RumbleData rumbleData)
        {
            tasks.Add(new RumbleTask()
            {
                rumbleData = rumbleData,
                timeLeft = rumbleData.duration
            });
        }

        public void SetTask(RumbleData rumbleData)
        {
            ClearTasks();
            AddTask(rumbleData);
        }

        public void RemoveTask(RumbleData rumbleData)
        {
            tasks.RemoveAll(task => task.rumbleData == rumbleData);
        }

        public void ClearTasks()
        {
            tasks.Clear();
        }
    }
}