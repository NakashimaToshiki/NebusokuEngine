using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NebusokuEngine.FaceEmotion
{

    public interface IEyeMorphView
    {
        float EyeOpenLeft { get; set; }
        float EyeOpenRight { get; set; }
        float EyeHalfOpenLeft { get; set; }
        float EyeHalfOpenRight { get; set; }
        float SmailLeft { get; set; }
        float SmailRight { get; set; }
    }

    public class EyeMorphController
    {

        private readonly IEyeMorphService _service;
        private readonly IEyeBlinkService _morph;
        private readonly IEyeMorphView _view;

        public EyeMorphController(IEyeMorphService service, IEyeBlinkService morph, IEyeMorphView view)
        {
            _service = service;
            _morph = morph;
            _view = view;
        }

        public void Update()
        {
            _service.BlinkUpdate();

            // 左目
            _morph.MorphUpdate(_service.BlinkLeft, _service.BlinkRight);
            _view.EyeHalfOpenLeft = _morph.EyeHalfOpenLeft;
            _view.EyeOpenLeft = _morph.EyeOpenLeft;

            _view.EyeHalfOpenRight = _morph.EyeHalfOpenRight;
            _view.EyeOpenRight = _morph.EyeOpenRight;

        }
    }
}