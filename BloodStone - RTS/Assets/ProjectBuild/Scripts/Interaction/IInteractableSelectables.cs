using Select;
using System.Collections.Generic;

namespace Interaction
{
    public interface IInteractableSelectables
	{
		void Interact(IReadOnlyList<ISelectable> selectables);
	}

}