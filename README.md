# RFID-HW-1 專案說明

## EasyPOD.dll 使用說明

1. **EasyPOD.dll 不放入 Git**
   - 請勿將 `EasyPOD.dll` 加入 Git 版本控制（已在 `.gitignore` 設定排除）。

2. **手動複製 EasyPOD.dll**
   - 請手動將 `EasyPOD.dll` 複製到專案根目錄下的 `libs` 資料夾。
   - 路徑範例：  
     ```
     MyProject/
      libs/
      WindowsFormsApplication6.csproj
     ```

3. **Build 時自動複製**
   - 專案在建置（Build）時，會自動將 `libs\EasyPOD.dll` 複製到輸出資料夾（`bin\Debug` 或 `bin\Release`），並放在輸出資料夾的根目錄。
   - 不需手動複製到 `bin` 目錄，建置流程會自動處理。

---