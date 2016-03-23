﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TileResponder : MonoBehaviour, IPointerClickHandler
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMouseClick()
    {
        UIManager.Instance.DeactivateEnemyPanel();

        Player currentPlayer = PlayerManager.Instance.getCurrentPlayer();

        // if there is a selected unit in moving state, gtfo dont process stuff
        if (currentPlayer.selectedObject != null)
        {
            if (currentPlayer.selectedObject.tag == "Unit")
            {
                if (currentPlayer.selectedObject.GetComponent<Unit>().state == Unit.UnitState.Moving)
                {
                    return;
                }
            }
        }

        // If an object is currently selected, check if unit and then move to current tile
        if (currentPlayer.selectedObject != null)
        {
            if (currentPlayer.selectedObject.tag == "Unit")
            {
                // if the tile is marked as traversable travel to it
                Vector2i tilePos = GLOBAL.worldToGrid(transform.position);

                if (TileMarker.Instance.travTiles.ContainsKey(tilePos))
                {
                    currentPlayer.selectedObject.GetComponent<Unit>().TravelToPos(tilePos);
                    TileMarker.Instance.Clear();
                }

                // if the tile is marked with an AoE tile, begin AoE selection
                else if (TileMarker.Instance.AoETiles.ContainsKey(tilePos))
                {
                    // STORE ATTACK ROOT LOCATION AND OPEN AoE WEAPON SELECT
                    // AoE weapon select is an aesthetic duplicate of the regular weapon select, but has behaviours specific to AoE sequence
                    GameObject.Find("CombatSequence").GetComponent<CombatSequence>().AoEWeaponSelect(GLOBAL.worldToGrid(transform.position));

                    TileMarker.Instance.Clear(); // clear purple tiles
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMouseClick();
    }
}