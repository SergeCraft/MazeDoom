using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.UI.BallTiltControl
{
    public class SwitchJoystickModeButtonController : MonoBehaviour
    {
        private Coroutine autoSetupInProgressCoroutine;
        private Coroutine autoSetupFinishedCoroutine;
        private float autoSetupFinishedPeriod = 1.0f;


        public void OnAutoSetupStateChanged(bool isAutoSetupInProgress)
        {
            if (isAutoSetupInProgress)
            {
                if (autoSetupFinishedCoroutine != null) StopCoroutine(autoSetupFinishedCoroutine);
                while(!enabled) new WaitForSeconds(0.1f);
                autoSetupInProgressCoroutine = StartCoroutine(AutoSetupInProgress());
            }
            else
            {
                if (autoSetupInProgressCoroutine != null) StopCoroutine(autoSetupInProgressCoroutine);
                autoSetupFinishedCoroutine = StartCoroutine(AutoSetupFinished());
            }
        }

        private IEnumerator AutoSetupInProgress()
        {
            GetComponentInChildren<TMP_Text>().text = "Hold on...";
            var buttonBg = GetComponent<Button>().image;
            Color minPulseColor = Color.red;
            minPulseColor.a = 0.6f;
            Color maxPulseColor = Color.yellow;
            maxPulseColor.a = 0.6f;
            float pulsePeriod = 0.3f;

            buttonBg.color = minPulseColor;
            while (true)
            {
                if (buttonBg.color != maxPulseColor)
                {
                    buttonBg.color += (maxPulseColor - minPulseColor) * (0.04f / pulsePeriod);
                }
                else
                {
                    Color tempColor = minPulseColor;
                    minPulseColor = maxPulseColor;
                    maxPulseColor = tempColor;
                }
                yield return new WaitForSeconds(0.04f);
            }
        }

        private IEnumerator AutoSetupFinished()
        {
            GetComponentInChildren<TMP_Text>().text = "OK";
            float startTime = Time.time;
            var buttonBg = GetComponent<Button>().image;
            Color targetColor = Color.green;
            targetColor.a = 0.6f;

            while (Time.time < startTime + autoSetupFinishedPeriod)
            {
                targetColor.a = 1 - (Time.time - startTime) / autoSetupFinishedPeriod;
                buttonBg.color = targetColor;
                yield return new WaitForSeconds(0.04f);
            }

            targetColor.a = 0f;
            buttonBg.color = targetColor;
            GetComponentInChildren<TMP_Text>().text = "Switch to joystick";
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
