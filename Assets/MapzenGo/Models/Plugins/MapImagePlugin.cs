using System;
using System.Collections.Generic;
using Assets.MapzenGo.Models.Plugins;
using MapzenGo.Models.Plugins;
using UniRx;
using UnityEngine;

namespace MapzenGo.Models.Plugins
{
    public class MapImagePlugin : Plugin
    {
        public string MapImageUrlBase = "http://b.tile.openstreetmap.org/";

        public override void Create(Tile tile)
        {
            base.Create(tile);
            
            var go = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
            go.name = "map";
            go.SetParent(tile.transform, true);
            go.localScale = new Vector3((float)tile.Rect.Width, (float)tile.Rect.Width, 1);
            go.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
            go.localPosition = Vector3.zero;
            go.localPosition -= new Vector3(0, 1, 0);
            var rend = go.GetComponent<Renderer>();
            rend.material = tile.Material;
            var url = MapImageUrlBase + tile.Zoom + "/" + tile.TileTms.x + "/" + tile.TileTms.y + ".png";
            ObservableWWW.GetWWW(url).Subscribe(
                success =>
                {
                    if (rend)
                    {
                        GameObject world = GameObject.FindWithTag("OpenWorld");
                        if (world.GetComponent<CachedDynamicTileManager>().Zoom < 14 && GameObject.Find("GameController").GetComponent<controllerVariables>().questTiles.Contains(go.parent.name))
                        {
                            rend.material.color = new Color(1f, 1f, 1f, 1f);

                        }
                        else if (world.GetComponent<CachedDynamicTileManager>().Zoom < 14)
                        {
                            rend.material.color = new Color(0.65f, 0.65f, 0.65f, 1);
                        }
                        else
                        {
                            rend.material.color = new Color(1f, 1f, 1f, 1f);
                        }
                        rend.material.mainTexture = new Texture2D(512, 512, TextureFormat.DXT5, false);
                        success.LoadImageIntoTexture((Texture2D)rend.material.mainTexture);
                    }
                },
                error =>
                {
                    Debug.Log(error);
                });

        }
    }
}
