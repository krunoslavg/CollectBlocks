    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utillity
{
    private int _fpsCounter = 0;
    private float testTimeStart = 0f;

    public static T GetRandomListItem<T>(List<T> p_list)
    {
        if (p_list != null && p_list.Count > 0)
        {
            return (p_list[Random.Range(0, p_list.Count)]);
        }
        return (T)(new object());
    }

    public void FPSCounter(Text p_FPSText)
    {
        if (p_FPSText == null)
            return;

        if (_fpsCounter == 0)
            testTimeStart = Time.time;

        if (Time.time <= testTimeStart + 1)
        {
            _fpsCounter++;
        }
        else
        {
            p_FPSText.text = _fpsCounter.ToString();
            _fpsCounter = 0;
        }
    }

    /// <summary>
    /// If enabled, allows us to speed up (F6 key) and slow down game (F5 key).
    /// </summary>
    public static void TimeFlowControl()
    {

        if (Input.GetKey(KeyCode.F5))
        {
            Time.timeScale = 0.125f;
        }
        else if (Input.GetKey(KeyCode.F6))
        {
            Time.timeScale = 3f;
        }
        else if (Input.GetKey(KeyCode.F7) || Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 40f;
        }
        else if (Time.timeScale != 1f)
            Time.timeScale = 1f;
    }

    public static IEnumerator FadeAlpha(Image p_imageToFade, float p_targetAlpha, float p_fadeDelta = 0.01f)
    {
        p_imageToFade.gameObject.SetActive(true);
        Color l_tempColor = p_imageToFade.color;
        float l_startingAlpha = p_imageToFade.color.a;
        float l_tempAlpha = 0f;
        float l_interpolizer = 0f;

        while (true)
        {
            if (p_imageToFade.color.a == p_targetAlpha)
            {
                if (p_targetAlpha <= 0f)
                    p_imageToFade.gameObject.SetActive(false);
                yield break;
            }

            l_interpolizer += p_fadeDelta * Time.deltaTime;

            l_tempAlpha = Mathf.Lerp(l_startingAlpha, p_targetAlpha, l_interpolizer);
            l_tempColor.a = l_tempAlpha;
            p_imageToFade.color = l_tempColor;
            yield return null;
        }
    }

    public static IEnumerator FadeAlpha(SpriteRenderer p_imageToFade, float p_targetAlpha, float p_fadeDelta = 0.01f, bool p_disablePostFading = true)
    {
        p_imageToFade.gameObject.SetActive(true);
        Color l_tempColor = p_imageToFade.color;
        float l_startingAlpha = p_imageToFade.color.a;
        float l_tempAlpha = 0f;
        float l_interpolizer = 0f;

        while (true)
        {
            if (p_imageToFade.color.a == p_targetAlpha)
            {
                if (p_targetAlpha <= 0f && p_disablePostFading)
                    p_imageToFade.gameObject.SetActive(false);
                yield break;
            }

            l_interpolizer += p_fadeDelta * Time.deltaTime;

            l_tempAlpha = Mathf.Lerp(l_startingAlpha, p_targetAlpha, l_interpolizer);
            l_tempColor.a = l_tempAlpha;
            p_imageToFade.color = l_tempColor;
            yield return null;
        }
    }

    public static IEnumerator FadeAlpha(Text p_text, float p_targetAlpha, float p_fadeDelta = 0.01f)
    {
        Color l_tempColor = p_text.color;
        float l_startingAlpha = p_text.color.a;
        float l_tempAlpha = 0f;
        float l_interpolizer = 0f;

        while (true)
        {
            if (p_text.color.a == p_targetAlpha)            
                yield break;            

            l_interpolizer += p_fadeDelta * Time.deltaTime;

            l_tempAlpha = Mathf.Lerp(l_startingAlpha, p_targetAlpha, l_interpolizer);
            l_tempColor.a = l_tempAlpha;
            p_text.color = l_tempColor;
            yield return null;
        }
    }

    public static IEnumerator FadeMusic(AudioSource p_audioSource, float p_value, float p_fadeSpeed)
    {
        float l_direction = -1.0f;

        if (p_value > p_audioSource.volume)
            l_direction = 1.0f;

        while (p_audioSource.volume != p_value)
        {
            p_audioSource.volume += (p_fadeSpeed * Time.deltaTime * l_direction);
            yield return null;
        }
    }
    /// <summary>
    /// Returns random float nubmer between 0 and 100.
    /// </summary>
    /// <returns>The generator.</returns>
    public static float ChanceGenerator()
    {
        return Random.Range(0, 100);
    }

    public static float Distance(Vector3 vectorA, Vector3 vectorB)
    {
        float l_distance = Abs(Vector3.Distance(vectorA, vectorB));
        return l_distance;
    }

    public static float DeltaAbsFloat(float p_num1, float p_num2)
    {
        return Abs(p_num1 - p_num2);       
    }

    public static float Abs(float value)
    {
        float absoluteValue = (value < 0) ? -value : value;
        return absoluteValue;
    }

    /// <summary>
    /// Scale the specified objectToScale by scaleAmount. Third parameter, axisIndex, lets us choose which axis should be scaled.
    /// </summary>
    /// <param name="objectToScale">Object to scale.</param>
    /// <param name="scaleAmount">Scale amount.</param>
    /// <param name="axisIndex">Axis index.</param>
    public static Vector3 Scale(Transform objectToScale, float scaleAmount, int axisIndex)
    {
        Vector3 _tempScaleVector = objectToScale.localScale;

        if (axisIndex == 1)
            _tempScaleVector.x += (scaleAmount * Time.deltaTime);
        if (axisIndex == 2)
            _tempScaleVector.y += (scaleAmount * Time.deltaTime);
        if (axisIndex == 3)
            _tempScaleVector.z += (scaleAmount * Time.deltaTime);

        return _tempScaleVector;
    }
    
    public static Vector3 ReturnPositionFromRect(RectTransform p_rectTransform, bool p_skipZCoordinate)
    {
        if (p_rectTransform == null)
        {
            //Debug.LogError ("Print rect transform is null!");
            return p_rectTransform.position;
        }
        Vector3 l_tempPosition = Camera.main.ScreenToWorldPoint(p_rectTransform.position);
        //print ("Rect transform position in world position: " + l_tempPosition);
        return l_tempPosition;
    }

    public static Vector3 ReturnPositionFromRect(RectTransform p_rectTransform)
    {
        return ReturnPositionFromRect(p_rectTransform, true);
    }

     public static List<Vector2> CaculateParabolaPathVert(Vector3 p_sourcePosition, Vector3 p_targetPosition, float p_horizontalRatio, float p_peakHeight, float p_waypointDensity = 0.1f)
     {
        List<Vector2> l_controlPoints = new List<Vector2>();
        List<Vector2> l_result = new List<Vector2>();
        Vector3 l_waypointPosition = Vector3.zero;
        float l_temp = 0f;

        l_controlPoints.Add(p_sourcePosition);
        l_controlPoints.Add(Vector3.zero);
        l_controlPoints.Add(p_targetPosition);

        l_temp = (p_sourcePosition.x < p_targetPosition.x) ? p_targetPosition.x - p_sourcePosition.x : p_sourcePosition.x - p_targetPosition.x;

        if (p_horizontalRatio < 0f)
            p_horizontalRatio = 0f;

        if (p_horizontalRatio > 1f)
            p_horizontalRatio = 1f;

        p_horizontalRatio = (p_sourcePosition.x > p_targetPosition.x) ? p_horizontalRatio *= -1f : p_horizontalRatio;

        l_controlPoints[1] = new Vector3(p_sourcePosition.x + (l_temp * p_horizontalRatio), p_targetPosition.y + p_peakHeight, 0f);

        l_result.Add(p_sourcePosition);

        if (l_controlPoints != null && l_controlPoints.Count > 1)
        {
            for (float t = 0; t <= 1; t += p_waypointDensity)
            {
                l_waypointPosition =
                    Mathf.Pow(1 - t, 3) * l_controlPoints[0] +
                    Mathf.Pow(1 - t, 2) * t * l_controlPoints[1] * 3 +
                    Mathf.Pow(t, 2) * (1 - t) * l_controlPoints[1] * 3 +
                    Mathf.Pow(t, 3) * l_controlPoints[2];

                l_result.Add(new Vector3(l_waypointPosition.x, l_waypointPosition.y));
            }
        }

        l_result.Add(p_targetPosition);

        return l_result;
    }
    
    public static List<Vector2> CaculateParabolaPathHorz(Vector3 p_sourcePosition, Vector3 p_targetPosition, float p_horizontalRatio, float p_peakHeight, float p_waypointDensity = 0.1f)
    {
        List<Vector2> l_controlPoints = new List<Vector2>();
        List<Vector2> l_result = new List<Vector2>();
        Vector2 l_waypointPosition = Vector2.zero;
        float l_temp = 0f;

        l_controlPoints.Add(p_sourcePosition);
        l_controlPoints.Add(Vector2.zero);
        l_controlPoints.Add(p_targetPosition);

        l_temp = (p_sourcePosition.y < p_targetPosition.y) ? p_targetPosition.y - p_sourcePosition.y : p_sourcePosition.y - p_targetPosition.y;

        if (p_horizontalRatio < 0f)
            p_horizontalRatio = 0f;

        if (p_horizontalRatio > 1f)
            p_horizontalRatio = 1f;

        p_horizontalRatio = (p_sourcePosition.y < p_targetPosition.y) ? p_horizontalRatio *= -1f : p_horizontalRatio;

        l_controlPoints[1] = new Vector2(p_sourcePosition.x + p_peakHeight, p_targetPosition.y + (l_temp * p_horizontalRatio));

        l_result.Add(p_sourcePosition);

        if (l_controlPoints != null && l_controlPoints.Count > 1)
        {
            for (float t = 0; t <= 1; t += p_waypointDensity)
            {
                l_waypointPosition =
                    Mathf.Pow(1 - t, 3) * l_controlPoints[0] +
                    Mathf.Pow(1 - t, 2) * t * l_controlPoints[1] * 3 +
                    Mathf.Pow(t, 2) * (1 - t) * l_controlPoints[1] * 3 +
                    Mathf.Pow(t, 3) * l_controlPoints[2];

                l_result.Add(new Vector2(l_waypointPosition.x, l_waypointPosition.y));
            }
        }

        l_result.Add(p_targetPosition);

        return l_result;
    } 


    //-/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    //-/////////////////////////////////////////////// LOOKAT2D() SECTION ///////////////////////////////////////////////////////////////
    //-//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static Quaternion LookAt2D(Transform p_objectToRotate, Vector3 p_targetPosition, Vector3 p_axis, bool p_reverseAngle, float p_angleFix)
    {
        Vector3 _cos = new Vector3();
        bool _reverseAngle = p_reverseAngle;
        float _angle = 0f;
        float _angleFix = 0f;

        if (p_targetPosition.y > p_objectToRotate.position.y)
        {
            _cos = p_targetPosition - p_objectToRotate.position;
            _angleFix = 0f;
            _angleFix += p_angleFix;
        }
        else
        {
            _angleFix = -180f;
            _angleFix += p_angleFix;
            _cos = p_objectToRotate.position - p_targetPosition;
        }

        if (p_objectToRotate.position.x != p_targetPosition.x)
        {

            _angle = Mathf.Acos(_cos.x / _cos.magnitude) * Mathf.Rad2Deg;

            if (_reverseAngle)
                _angleFix += 180f;

            return Quaternion.AngleAxis(_angle + _angleFix, p_axis);
        }
        else
            return p_objectToRotate.rotation;
    }

    /*
    public void LookAt2D (Transform objectToRotate, Vector3 targetPosition, bool reverseAngle, float angleFix = 0f) 
    {
    	LookAt2D (objectToRotate, targetPosition, objectToRotate.forward, reverseAngle, angleFix); 
    }

    public 	void LookAt2D (Transform objectToRotate, Vector3 targetPosition, float angleFix = 0f) 
    {
    	LookAt2D(objectToRotate, targetPosition, objectToRotate.forward, false, angleFix);
    }*/
    public static Vector3 SetRandomPoint(Vector2 p_xRange, Vector2 p_yRange)
    {
        return SetRandomPoint(p_xRange, p_yRange, true);
    }

    /// <summary>
    /// Returns Vector3 whose x component is random value of x,y components of first Vector2 in parametar and y component is also random value of x,y components of second Vector2 from parametar. Z component is zero.
    /// </summary>
    public static Vector3 SetRandomPoint(Vector2 p_xRange, Vector2 p_yRange, bool p_random)
    {
        if (p_random)
        {
            //print ("GM xRange value: " + xRange);
            //print ("Gm yRange value: " + yRange);
            int x = Random.Range(0, 100);

            if (x < 50)
            {
                p_xRange *= 1;
                p_yRange *= 1;
            }
            else
            {
                p_xRange *= -1;
                p_yRange *= -1;
            }
        }

        return new Vector3(p_xRange.RandomValue(), p_yRange.RandomValue(), 0f);
    }

    public static IEnumerator CheckDisabledObjects(Transform p_childrenParent)
    {
        while (true)
        {
            bool l_noneLeft = Utillity.CheckForDisabledChildEntities(p_childrenParent);
            if (l_noneLeft)
                yield break;
            yield return new WaitForSeconds(0.25f);
        }
    }

    public static IEnumerator CheckAreGameEntitiesInDisabledState(List<GameEntity> p_gameEntityList)
    {
        if (p_gameEntityList == null || p_gameEntityList.Count < 1)
        {
            Debug.LogError("<LevelManager> ().CheckDisabledGameEntities(): Given entity list is empty or null!!");
            yield break;
        }

        while (true)
        {
            bool l_nonLeftActive = Utillity.CheckForDisabledGameEntities(p_gameEntityList);
            if (l_nonLeftActive)
                yield break;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public static IEnumerator CheckAreGameEntitiesInState(List<GameEntity> p_entitylist, StateType p_stateType)
    {
        if (p_entitylist == null || p_entitylist.Count < 1)
        {
            Debug.LogError("<LevelManager> ().CheckDisabledGameEntities(): Given entity list is empty or null!!");
            yield break;
        }

        while (true)
        {
            // If it doesnt find any entity in state type provided, breaks from coroutine.
            bool l_nonLeft = !Utillity.CheckForGameEntitiesWithStateType(p_entitylist, p_stateType);
            if (l_nonLeft)
                yield break;
            yield return new WaitForSeconds(0.05f);
        }
    }


    public static bool CheckForGameEntitiesWithStateType(List<GameEntity> p_entityList, StateType p_stateType)
    {
        if (p_entityList == null)
        {
            Debug.Log("Utillity.CheckForGameEntitiesWithStateType(): Provided entity object list is null!");
            return true;
        }

        int l_size = p_entityList.Count;

        if (l_size < 1)
        {
            Debug.Log("Utillity.CheckForGameEntitiesWithStateType(): There are no child elements in list: " + p_entityList);
            return true;
        }

        bool l_someLeft = !Utillity.CheckIsAnyEntityNotInState(p_entityList, p_stateType);

        return l_someLeft;
    }

    public static bool CheckForDisabledGameEntities(List<GameEntity> p_entityList)
    {
        if (p_entityList == null)
        {
            Debug.Log("Utillity.CheckForDisabledObjects(): Provided entity object list is null!");
            return true;
        }

        int l_size = p_entityList.Count;

        if (l_size < 1)
        {
            Debug.Log("Utillity.CheckForDisabledObjects(): There are no child elements in list: " + p_entityList);
            return true;
        }

        bool l_noneLeftActive = !Utillity.CheckIsAnyEntityNotInState(p_entityList, StateType.Disabled);

        return l_noneLeftActive;
    }

    public static int CheckForDisabledGameEntitiesCount(List<GameEntity> p_entityList)
    {
        if (p_entityList == null)
        {
            Debug.Log("Utillity.CheckForDisabledObjects(): Provided entity object list is null!");
            return 0;
        }

        int l_size = p_entityList.Count;

        if (l_size < 1)
        {
            Debug.Log("Utillity.CheckForDisabledObjects(): There are no child elements in list: " + p_entityList);
            return 0;
        }

        int l_counter = 0;

        for (int i = 0; i < l_size; i++)
        { 
            if (p_entityList[i].StateController.IsEntityInState(StateType.Disabled))
            { 
                l_counter++; 
            }
        }
        return l_counter;
    }

    public static bool CheckForDisabledChildEntities(Transform p_parentTransform)
    {
        if (p_parentTransform == null)
        {
            //Debug.LogWarning("Utillity.CheckForDisabledObjects(): Provided transform is null!");
            return true;
        }

        int l_size = p_parentTransform.childCount;

        if (l_size < 1)
        {
            //Debug.LogWarning ("Utillity.CheckForDisabledObjects(): There are no child objects in game object: " + p_parentTransform.gameObject.name);
            return true;
        }

        bool l_value = false;

        for (int i = 0; i < l_size; i++)
        {
            // If one is found, we break from loop.
            Transform l_childTransform = p_parentTransform.GetChild(i);

            if (l_childTransform.IsActive())
            {
                i = 0;
                l_value = false;
                break;
            }
            else if (i >= l_size - 1)
            {
                l_value = true;
                break;
            }
        }
        return l_value;
    }

    public static bool CheckIsAnyEntityNotInState(List<GameEntity> p_entitylist, StateType p_stateType)
    {
        if (p_entitylist == null)
            return false;

        int l_size = p_entitylist.Count;

        if (l_size < 1)
            return false;

        bool l_value = false;

        for (int i = 0; i < l_size; i++)
        {
            // If one is found, we break from loop.
            if (!p_entitylist[i].StateController.IsEntityInState(p_stateType))
            {
                i = 0;
                l_value = true;
               break;
            }
        }
        return l_value;
    }

    public static GameEntity GetFirstDisabledEntity(List<GameEntity> p_entitylist)
    {
        if (p_entitylist == null)
            return null;

        int l_size = p_entitylist.Count;

        if (l_size < 1)
            return null;

        for (int i = 0; i < l_size; i++)
        {
            if (p_entitylist[i].StateController.IsEntityInState(StateType.Disabled))            
                return p_entitylist[i];                                     
        }
        return null;
    }

    public static void ToggleEntitiesMainState(List<GameEntity> p_gameEntityList, bool p_toggleInto = true)
    {
        int listLen = p_gameEntityList.Count;
        for (int i = 0; i < listLen; i++)
        {
            p_gameEntityList[i].StateController.ToggleMainState(p_toggleInto);
        }
    }

    public static void ToggleChildEntities(Transform p_clonesParent, bool p_value, GameObject p_objectToSkip)
    {
        int l_size = p_clonesParent.childCount;

        if (l_size < 1)
        {
            //Debug.LogError("There are " + l_size + " child objects in game object: " + p_clonesParent.gameObject.name);
            return;
        }

        for (int i = 0; i < l_size; i++)
        {
            GameObject l_childObject = p_clonesParent.GetChild(i).gameObject;

            if (p_objectToSkip != null && l_childObject == p_objectToSkip)
            {
                continue;
            }
            // If one is found, we break from loop. 
            if (l_childObject.activeInHierarchy)
            {
                l_childObject.SetActive(p_value);
            }
        }
    }

    public static void ToggleChildEntities(Transform p_clonesParent, bool p_value)
    {
        ToggleChildEntities(p_clonesParent, p_value, null);
    }

    // Really, really slow. Used only in emergency cases like when shooting video demo, so we need as little bit of code to access our entities 
    // but simple and straightforward, without much complication, meaning avoid usual paths of accessing entites and more proper way, ofc.
    public static List<GameEntity> FindEntitiesOfType(StateControllerType p_type)
    {
        List<GameEntity> l_list = new List<GameEntity>();

        foreach (GameEntityStateController l_object in Object.FindObjectsOfType<GameEntityStateController>())
        {
            if (l_object.StateControllerType != p_type)
                continue;

            l_list.Add(l_object.GameEntity);
        }
        return l_list;
    }

    public static GameEntity FindEntityOfType(StateControllerType p_type)
    {
        foreach (GameEntityStateController l_object in Object.FindObjectsOfType<GameEntityStateController>())
        {
            if (l_object.StateControllerType != p_type)
                continue;

            return l_object.GameEntity;
        }
        return null;
    } 
}