using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public class CharacterService : MonoBehaviour
    {
        /*
         NOTE:
         Because we want to keep the class non-static, I created an instance 
         within the class so that it can be accessed by components of the character class.
         It could be improved by making the class static, So that we don't have to create an instance of this, 
         but since I don't know if that's what we want to continue using it as an instance from scene to scene, 
         I'll maintain the convention.
         */
        public static CharacterService Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        private readonly Dictionary<string, ICharacter> _charactersById = new();
        public bool TryAddCharacter(string id, ICharacter character)
            => _charactersById.TryAdd(id, character);
        public bool TryRemoveCharacter(string id)
            => _charactersById.Remove(id);
        public ICharacter GetCharacterService(string id)
            => _charactersById.TryGetValue(id, out var character) ? character : null;
    }
}
