using System.Collections.Generic;
using UnityEngine;

namespace RGSMS
{
    [System.Serializable]
    public sealed class InputsContainer
    {
        [SerializeReference]
        private List<InputConfig> _inputs = null;

        public List<InputConfig> GetInputs() => _inputs;
    }
}
