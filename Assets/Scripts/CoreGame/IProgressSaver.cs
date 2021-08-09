using System.Collections.Generic;

public interface IProgressSaver {
  List<int> LoadSideDate();
  void SaveSide(List<int> sidesNums);
  void ClearSideProgress();
}