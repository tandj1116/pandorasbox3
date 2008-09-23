using System;

namespace TheBox.BoxServer
{
	public enum BoxMessages
	{
		// Errors
		GenericError,
		NotSupported,
		MobileNotOnline,
		DataNotAvailable,

		// Login Managment
		Login,
		LoginOk,
		WrongCredentials,
		AccessLevelError,

		// Data files
		BoxData,
		SpawnData,
		PropData,

		// Misc actions
		Spawn,

		// Remote explorer
		RemoteExplorer,
		NotRegistered,
		FileNotFound,
		Upload,
		Download,
		Rename,
		Delete,
		CreateFolder,
		ExtensionError
	}
}
