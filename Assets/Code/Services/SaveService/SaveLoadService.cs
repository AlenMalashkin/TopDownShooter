using System.Collections.Generic;
using Code.Data.Progress;
using Code.Services.ProgressService;
using UnityEngine;

namespace Code.Services.SaveService
{
    public class SaveLoadService : ISaveLoadService
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        
        private IProgressService _progressService;
        
        public SaveLoadService(IProgressService progressService)
        {
            _progressService = progressService;
        }

        public Progress LoadProgress()
            => JsonUtility.FromJson<Progress>(PlayerPrefs.GetString("Progress"));

        public void SaveProgress()
        {
            PlayerPrefs.SetString("Progress", JsonUtility.ToJson(_progressService.Progress));

            foreach (var progressReader in ProgressReaders)
                progressReader.ReadProgress(_progressService.Progress);
        }
    }
}