using USCG.Core.Telemetry;

using UnityEngine;

public class RecordCombatMetrics : MonoBehaviour
{
    // Keep a reference to the metrics we create.
    private MetricId _dropFailMetric = default;
    private MetricId _clickSucceedMetric = default;
    private MetricId _clickTotalMetric = default;
    private MetricId _dropTotalMetric = default;
    private MetricId _vanguardChangeMetric = default;
    private MetricId _counterDamageMetric = default;
    private MetricId _attackDamageMetric = default;
    private MetricId _deltaTimeMetric = default;
    private MetricId _numberOfTries = default;

    private void Start()
    {
        // Create all metrics in Start().
        _dropFailMetric = TelemetryManager.instance.CreateSampledMetric<Vector3>("DropFailMetric");
        _clickSucceedMetric = TelemetryManager.instance.CreateSampledMetric<Vector3>("ClickSucceedMetric");
        _clickTotalMetric = TelemetryManager.instance.CreateSampledMetric<Vector3>("ClickTotalMetric");
        _dropTotalMetric = TelemetryManager.instance.CreateSampledMetric<Vector3>("DropTotalMetric");
        _vanguardChangeMetric = TelemetryManager.instance.CreateAccumulatedMetric("VanguardChangeMetric");
        _counterDamageMetric = TelemetryManager.instance.CreateAccumulatedMetric("CounterDamageMetric");
        _attackDamageMetric = TelemetryManager.instance.CreateAccumulatedMetric("AttackDamageMetric");
        _deltaTimeMetric = TelemetryManager.instance.CreateSampledMetric<float>("TotalFightTime");
        _numberOfTries = TelemetryManager.instance.CreateAccumulatedMetric("NumberOfTries");
    }

    private void Update()
    {

    }
    public void Drop(Vector3 input)
    {
        TelemetryManager.instance.AddMetricSample(_dropTotalMetric, input);
    }

    public void ClickSucceed(Vector3 input)
    {
        TelemetryManager.instance.AddMetricSample(_clickSucceedMetric, input);
    }

    public void ClickTotal(Vector3 input)
    {
        TelemetryManager.instance.AddMetricSample(_clickTotalMetric, input);
    }

    public void FailedDrop(Vector3 input)
    {
        TelemetryManager.instance.AddMetricSample(_dropFailMetric, input);
    }

    public void EndFightTime(float input)
    {
        TelemetryManager.instance.AddMetricSample(_deltaTimeMetric, input);
        TelemetryManager.instance.AccumulateMetric(_numberOfTries, 1);
    }

    public void ChangedVanguard()
    {
        TelemetryManager.instance.AccumulateMetric(_vanguardChangeMetric, 1);
    }

    public void CounterAttack(int input)
    {        
        TelemetryManager.instance.AccumulateMetric(_counterDamageMetric, input);
    }

    public void Attack(int input)
    {        
        TelemetryManager.instance.AccumulateMetric(_attackDamageMetric, input);
    }
}