using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

public class GC_Checker : MonoBehaviour
{
    private ProfilerRecorder _gcRecoder;

    private void OnEnable()
    {
        _gcRecoder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Collect Count");

    }
    void OnDisable()
    {
        _gcRecoder.Dispose();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (_gcRecoder.LastValue > 0)
        {
            Debug.LogWarning($"GC 발생: {_gcRecoder.LastValue} 회 (프레임 {Time.frameCount})");
        }
    }
}
