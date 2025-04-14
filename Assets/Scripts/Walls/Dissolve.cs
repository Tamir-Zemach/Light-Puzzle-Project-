using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DissolveExample
{
    public class Dissolve : MonoBehaviour
    {
        // Start is call ed before the first frame update
        List<Material> materials = new List<Material>();
        
        [SerializeField] private float _dissolveTime;
        private float _currentDissolveValue;
        private const float _maxDissolveValue = 1;
        private const float _minDissolveValue = 0;
        private Coroutine currentCoroutine;

        //void Awake()
        //{
        //    var renders = GetComponentsInChildren<Renderer>();

        //    for (int i = 0; i < renders.Length; i++)
        //    {
        //        if (renders[i].TryGetComponent<ParticleSystem>(out ParticleSystem p) == false)
        //        {
        //            foreach (var material in renders[i].materials)
        //            {
        //                // Check if the material has the "_Dissolve" property
        //                if (material.HasProperty("_Dissolve"))
        //                {
        //                    materials.Add(material);
        //                }
        //            }
        //        }
        //    }
        //}


        // does the same purpse - but optimaized
        void Awake()
        {
            materials.AddRange(
                GetComponentsInChildren<Renderer>()
                .Where(r => !(r is ParticleSystemRenderer))
                .SelectMany(r => r.materials)
                .Where(m => m.HasProperty("_Dissolve"))
            );
        }
        public void _Dissolve()
        {
            StopAllCoroutines();
            GetDissolveValue();
            currentCoroutine = StartCoroutine(AddValueWithLerp());
        }
        IEnumerator AddValueWithLerp()
        {
            float timepassed = 0;

            while (timepassed < _dissolveTime)
            {
                float t = timepassed / _dissolveTime;
                var value = Mathf.Lerp(_currentDissolveValue, _maxDissolveValue, t);
                timepassed += Time.deltaTime;
                SetDissolveValue(value);
                yield return null;
            }
            SetDissolveValue(_maxDissolveValue);
        }
        public void UnDissolve()
        {
            StopAllCoroutines();
            GetDissolveValue();
            currentCoroutine = StartCoroutine(SubstractValueWithLerp());
        }
        IEnumerator SubstractValueWithLerp()
        {
            float timepassed = 0;
            while (timepassed < _dissolveTime)
            {
                float t = timepassed / _dissolveTime;
                var value = Mathf.Lerp(_currentDissolveValue, _minDissolveValue, t);
                timepassed += Time.deltaTime;
                SetDissolveValue(value);
                yield return null;
            }
            SetDissolveValue(_minDissolveValue);
        }
        public void SetDissolveValue(float value)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_Dissolve", value);
            }
        }
        public void GetDissolveValue()
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].HasProperty("_Dissolve"))
                {
                    _currentDissolveValue = materials[i].GetFloat("_Dissolve");
                }
            }
        }

    }
}