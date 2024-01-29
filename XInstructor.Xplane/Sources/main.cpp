#include <stdio.h>
#include <string.h>
#include <string>
#include <XPLM/XPLMDataAccess.h>

PLUGIN_API int XPluginStart(char* outName, char* outSig, char* outDesc)
{
	std::string name = "XInstrutor Connector";
	std::string signature = "rdessart.XInstrutor.Connector";
	std::string desc = "X-Instructor connector for X-Plane";

	strcpy_s(outName, name.length(), name.c_str());
	strcpy_s(outSig, signature.length(), signature.c_str());
	strcpy_s(outDesc, desc.length(), desc.c_str());

	return 1;
}

PLUGIN_API void	XPluginStop(void)
{
}

PLUGIN_API void XPluginDisable(void)
{
}

PLUGIN_API int XPluginEnable(void)
{
	return 1;
}

PLUGIN_API void XPluginReceiveMessage(XPLMPluginID inFromWho, int inMessage, void* inParam)
{
}