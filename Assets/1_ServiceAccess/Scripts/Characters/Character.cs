using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;

        protected virtual void OnEnable()
        {
            CharacterService.Instance.TryAddCharacter(id, this);
        }

        protected virtual void OnDisable()
        {
            CharacterService.Instance.TryRemoveCharacter(id);
        }
    }
}