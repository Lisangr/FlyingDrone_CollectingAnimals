using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 100f;
    public float minZoom = 2f;
    public float maxZoom = 70f;
    public Text zoomText;
    public Slider zoomSlider;
    public bool isZoomActive = true; // Флаг для контроля обновления зума

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        zoomSlider.minValue = 0f;
        zoomSlider.maxValue = 1f;
        zoomSlider.onValueChanged.AddListener(OnSliderValueChanged);
        zoomSlider.value = NormalizeZoom(cam.orthographic ? cam.orthographicSize : cam.fieldOfView);
        UpdateZoomDisplay();
    }

    void Update()
    {
        if (!isZoomActive) return; // Если флаг отключен, не обновлять зум

        if (cam.orthographic)
        {
            HandleOrthographicZoom();
        }
        else
        {
            HandlePerspectiveZoom();
        }

        UpdateZoomDisplay();
    }

    private void HandlePerspectiveZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float scrollAmount = Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - scrollAmount, minZoom, maxZoom);

            // Обновляем слайдер
            zoomSlider.value = NormalizeZoom(cam.fieldOfView);
        }
    }

    private void HandleOrthographicZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float scrollAmount = Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scrollAmount, minZoom, maxZoom);

            // Обновляем слайдер
            zoomSlider.value = NormalizeZoom(cam.orthographicSize);
        }
    }

    private void UpdateZoomDisplay()
    {
        float zoomFactor = 0;

        if (cam.orthographic)
        {
            zoomFactor = maxZoom / cam.orthographicSize;
        }
        else
        {
            zoomFactor = maxZoom / cam.fieldOfView;
        }

        zoomText.text = Mathf.Round(zoomFactor).ToString();
    }

    private void OnSliderValueChanged(float value)
    {
        if (!isZoomActive) return;

        float zoomValue = DenormalizeZoom(value);

        if (cam.orthographic)
        {
            cam.orthographicSize = zoomValue;
        }
        else
        {
            cam.fieldOfView = zoomValue;
        }

        UpdateZoomDisplay();
    }

    private float NormalizeZoom(float zoomValue)
    {
        // Нормализуем значение зума в диапазон от 0 до 1
        return Mathf.InverseLerp(maxZoom, minZoom, zoomValue);
    }

    private float DenormalizeZoom(float sliderValue)
    {
        // Переводим нормализованное значение слайдера обратно в диапазон зума
        return Mathf.Lerp(maxZoom, minZoom, sliderValue);
    }
}
