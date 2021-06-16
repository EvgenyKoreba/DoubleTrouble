using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEventSystem
{

    public interface IPickUpModifierHandler : IGlobalSubscriber
    {
        void ModifierPickUped(Modifier modifier);
    }

}
