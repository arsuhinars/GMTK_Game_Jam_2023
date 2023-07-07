using UnityEngine;

namespace GMTK_2023.Utils
{
    public static class TagUtils
    {
        public static bool CompareTagsFromArray(Component component, string[] tags)
        {
            for (int i = 0; i < tags.Length; i++)
            {
                if (component.CompareTag(tags[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
