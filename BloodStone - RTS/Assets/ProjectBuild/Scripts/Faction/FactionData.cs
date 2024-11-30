using Data;
using State;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Faction
{
    public class FactionData : MonoBehaviour
	{
		[field: SerializeField] public FactionType FactionType { get; private set; }
		[field: SerializeField] public EntityCollectionData CollectionData { get; private set; }
		[field: SerializeField] public List<UnitBase> Units { get; private set; }
		[field: SerializeField] public InteractionMode InteractionMode { get; private set; }

		public void ChangeInteractionMode(InteractionMode interactionMode)
		{
			InteractionMode = interactionMode;
		}

	}
}