using UnityEngine;

public class HexGridManager : Singleton<HexGridManager>
{
    [Header("Hex Grid Properties")] 
    public float size;

    [Header("Terrain Detection")] 
    [SerializeField] private LayerMask _terrainInclusiveLayer;
    [SerializeField] private LayerMask _terrainExclusiveLayer;

    #region Screen Calculations
    
    public Vector3 OffsetToScreen(Vector2Int offset)
    {
        var x = size * Mathf.Sqrt(3) * (offset.x + .5f * (offset.y & 1));
        var y = size * 3f / 2f * offset.y;
        return new Vector3(x, y, 0);
    }

    public Vector2Int ScreenToOffset(Vector3 position)
    {
        var frac = ScreenToFractionalAxial(position);
        var cube = CubeRound(FracAxialToCube(frac));
        return CubeToOffset(cube);
    }
    
    public Vector2 ScreenToFractionalAxial(Vector3 position)
    {
        var q = (position.x * Mathf.Sqrt(3)/3f - position.y / 3f) / size;
        var r = 2f / 3f * position.y / size;
        return new Vector2(q, r);
    }

    public Vector3 AxialToScreen(Vector2Int axial)
    {
        var x = size * (Mathf.Sqrt(3) * axial.x + Mathf.Sqrt(3)/2f * axial.y);
        var y = size * 3f / 2f * axial.y;
        return new Vector3(x, y, 0);
    }

    public Vector3Int CubeRound(Vector3 frac)
    {
        int q = Mathf.RoundToInt(frac.x);
        int r = Mathf.RoundToInt(frac.y);
        int s = Mathf.RoundToInt(frac.z);
        
        var q_diff = Mathf.Abs(q - frac.x);
        var r_diff = Mathf.Abs(r - frac.y);
        var s_diff = Mathf.Abs(s - frac.z);
        
        if(q_diff > r_diff && q_diff > s_diff)
            q = -r - s;
        else if(r_diff > s_diff)
            r = -q - s;
        else
            s = -q - r;

        return new Vector3Int(q, r, s);
    }

    #endregion
    

    #region Conversions
    
    public static Vector2Int CubeToOffset(Vector3Int cube)
    {
        var col = cube.x + (cube.y - (cube.y& 1)) / 2;
        var row = cube.y;
        return new Vector2Int(col, row);
    }
    
    public static Vector3Int OffsetToCube(Vector2Int offset)
    {
        var q = offset.x - (offset.y + (offset.y & 1)) / 2;
        var r = offset.y;
        return new Vector3Int(q, r, -q-r);
    }
    
    public static Vector2Int OffsetToAxial(Vector2Int offset)
    {
        var q = offset.x - (offset.y - (offset.y & 1)) / 2;
        var r = offset.y;
        return new Vector2Int(q, r);
    }
    
    public static Vector2Int AxialToOffset(Vector2Int axial)
    {
        var col = axial.x + (axial.y - (axial.y & 1)) / 2;
        var row = axial.y;
        return new Vector2Int(col, row);
    }

    public static Vector3Int AxialToCube(Vector2Int axial)
    {
        var q = axial.x;
        var r = axial.y;
        var s = -q - r;
        return new Vector3Int(q, r, s);
    }
    
    public static Vector2Int CubeToAxial(Vector3Int cube)
    {
        var q = cube.x;
        var r = cube.y;
        return new Vector2Int(q, r);
    }

    public static Vector3 FracAxialToCube(Vector2 frac)
    {
        var q = frac.x;
        var r = frac.y;
        var s = -q - r;
        return new Vector3(q, r, s);
    }

    #endregion
    

    public void IsTerrainFree(Vector2Int offset, out bool free)
    {
        float checkRadius = size * Mathf.Sqrt(3) / 2f;
        Vector3 position = OffsetToScreen(offset);
        bool @void = Physics2D.OverlapCircle(position, checkRadius, _terrainInclusiveLayer);
        bool blocked = Physics2D.OverlapCircle(position, checkRadius, _terrainExclusiveLayer);
        free = !@void && !blocked;
    }

    public Vector3 SnapToGrid(Vector3 position)
    {
        return OffsetToScreen(ScreenToOffset(position));
    }
}