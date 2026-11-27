@echo off
title Posada del Roble - Iniciando API y Windows Forms
cd /d "%~dp0"

echo [1/3] Iniciando servidor Hotel.Api en http://localhost:5158 ...
start "Hotel.Api (Servidor REST)" cmd /k "dotnet run --project Hotel.Api\Hotel.Api.csproj --urls http://localhost:5158"

timeout /t 3 /nobreak >nul

echo [2/3] Abriendo Portal Web de Reservaciones para Clientes en el navegador ...
start "" "http://localhost:5158"

echo [3/3] Abriendo interfaz Hotel.WindowsForms (Posada del Roble) ...
start "" "Hotel.WindowsForms\bin\Debug\net9.0-windows\Hotel.WindowsForms.exe"
