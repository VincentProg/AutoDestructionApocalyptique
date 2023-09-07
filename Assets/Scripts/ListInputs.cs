using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inputs", menuName = "AutoDestructionInputs/Add inputs")]
public class ListInputs : ScriptableObject
{
    [SerializeField] List<PairInputs> _listPairInputs;

    public List<PairInputs> ListPairInputs { get => _listPairInputs; }
}
