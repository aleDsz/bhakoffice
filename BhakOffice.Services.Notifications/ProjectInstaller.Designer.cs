namespace BhakOffice.Services.Notifications {
  partial class ProjectInstaller {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.service_process_installer = new System.ServiceProcess.ServiceProcessInstaller();
      this.service_installer = new System.ServiceProcess.ServiceInstaller();
      // 
      // service_process_installer
      // 
      this.service_process_installer.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
      this.service_process_installer.Password = null;
      this.service_process_installer.Username = null;
      // 
      // service_installer
      // 
      this.service_installer.Description = "BhakOffice Notifications Micro Service";
      this.service_installer.DisplayName = "BhakOffice Notifications";
      this.service_installer.ServiceName = "BhakOffice Notifications";
      this.service_installer.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.service_process_installer,
            this.service_installer});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller service_process_installer;
    private System.ServiceProcess.ServiceInstaller service_installer;
  }
}