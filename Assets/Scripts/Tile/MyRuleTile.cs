using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "MyTiles/MyRuleTile")]
public class MyRuleTile : RuleTile
{
    /// <summary>
    /// StartUp is called on the first frame of the running Scene.
    /// </summary>
    /// <param name="position">Position of the Tile on the Tilemap.</param>
    /// <param name="tilemap">The Tilemap the tile is present on.</param>
    /// <param name="instantiatedGameObject">The GameObject instantiated for the Tile.</param>
    /// <returns>Whether StartUp was successful</returns>
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject instantiatedGameObject)
    {
        if (instantiatedGameObject != null)
        {
            Tilemap tmpMap = tilemap.GetComponent<Tilemap>();
            Matrix4x4 orientMatrix = tmpMap.orientationMatrix;

            var iden = Matrix4x4.identity;
            Vector3 gameObjectTranslation = new Vector3();
            Quaternion gameObjectRotation = new Quaternion();
            Vector3 gameObjectScale = new Vector3();

            bool ruleMatched = false;
            Matrix4x4 transform = iden;
            foreach (TilingRule rule in m_TilingRules)
            {
                if (RuleMatches(rule, position, tilemap, ref transform))
                {
                    transform = orientMatrix * transform;

                    // Converts the tile's translation, rotation, & scale matrix to values to be used by the instantiated GameObject
                    gameObjectTranslation = new Vector3(transform.m03, transform.m13, transform.m23);
                    gameObjectRotation = Quaternion.LookRotation(new Vector3(transform.m02, transform.m12, transform.m22), new Vector3(transform.m01, transform.m11, transform.m21));
                    gameObjectScale = transform.lossyScale;

                    ruleMatched = true;
                    break;
                }
            }
            if (!ruleMatched)
            {
                // Fallback to just using the orientMatrix for the translation, rotation, & scale values.
                gameObjectTranslation = new Vector3(orientMatrix.m03, orientMatrix.m13, orientMatrix.m23);
                gameObjectRotation = Quaternion.LookRotation(new Vector3(orientMatrix.m02, orientMatrix.m12, orientMatrix.m22), new Vector3(orientMatrix.m01, orientMatrix.m11, orientMatrix.m21));
                gameObjectScale = orientMatrix.lossyScale;
            }

            instantiatedGameObject.transform.localPosition = gameObjectTranslation + tmpMap.CellToLocalInterpolated(position + tmpMap.tileAnchor);
            instantiatedGameObject.transform.localRotation = gameObjectRotation;
            instantiatedGameObject.transform.localScale = gameObjectScale;

            // 将Tile位置与GameObject存储到Tilemap Gameobject
            TilemapGameobject tilemapGameobject = FindComponentInParents<TilemapGameobject>(instantiatedGameObject);
            tilemapGameobject.AddTileGameObject(position, instantiatedGameObject);
        }
        return true;
    }

    /// <summary>
    /// 沿着父层级查找，直到找到第一个指定类型的组件。
    /// </summary>
    /// <typeparam name="T">要查找的组件类型</typeparam>
    /// <param name="child">开始查找的子对象</param>
    /// <returns>找到的组件，如果没有找到返回null</returns>
    public static T FindComponentInParents<T>(GameObject child) where T : Component
    {
        Transform current = child.transform;
        while (current != null)
        {
            T component = current.GetComponent<T>();
            if (component != null)
            {
                return component;
            }
            current = current.parent;
        }
        return null;
    }

}

