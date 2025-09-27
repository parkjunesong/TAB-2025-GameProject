using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Inventory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<UnitData> pokemons = new List<UnitData>(6);
    private int maxPartySize = 6;

    public bool AddPokemon(UnitData newPokemon)
    {
        if (pokemons.Count >= maxPartySize)
        {
            Debug.Log("파티가 가득 찼습니다!");
            return false;
        }
        pokemons.Add(newPokemon);
        Debug.Log(newPokemon.name + " 이(가) 파티에 추가되었습니다.");
        return true;
    }

    public void RemovePokemon(UnitData pokemon)
    {
        if (pokemons.Contains(pokemon))
        {
            pokemons.Remove(pokemon);
            Debug.Log(pokemon.name + " 이(가) 파티에서 제거되었습니다.");
        }
    }

    public void SwapPokemon(int index, UnitData newPokemon)
    {
        if (index >= 0 && index < pokemons.Count)
        {
            Debug.Log(pokemons[index].name + " 이(가) " + newPokemon.name + " 으로 교체되었습니다.");
            pokemons[index] = newPokemon;
        }
    }
}
