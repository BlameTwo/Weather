Dim WshShell
Set WshShell = CreateObject("Wscript.Shell")
'WshShell.SendKeys "^%{RIGHT}"
key = chr(&h88B0)
WshShell.Sendkeys key