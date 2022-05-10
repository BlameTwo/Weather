Dim WshShell
Set WshShell = CreateObject("Wscript.Shell")
'WshShell.SendKeys "^%{RIGHT}"
key = chr(&h88B1)
WshShell.Sendkeys key