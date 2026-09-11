<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class defectHome
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(defectHome))
        Me.btnRegisterngs = New System.Windows.Forms.PictureBox()
        Me.btnAdjustngs = New System.Windows.Forms.PictureBox()
        Me.btnRegisterncs = New System.Windows.Forms.PictureBox()
        Me.btnAdjustncs = New System.Windows.Forms.PictureBox()
        Me.btnBackDefectHome = New System.Windows.Forms.PictureBox()
        Me.pbRefectData = New System.Windows.Forms.PictureBox()
        CType(Me.btnRegisterngs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdjustngs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRegisterncs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdjustncs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBackDefectHome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbRefectData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnRegisterngs
        '
        Me.btnRegisterngs.BackColor = System.Drawing.Color.Red
        Me.btnRegisterngs.Location = New System.Drawing.Point(14, 64)
        Me.btnRegisterngs.Name = "btnRegisterngs"
        Me.btnRegisterngs.Size = New System.Drawing.Size(50, 33)
        Me.btnRegisterngs.TabIndex = 1
        Me.btnRegisterngs.TabStop = False
        Me.btnRegisterngs.Visible = False
        '
        'btnAdjustngs
        '
        Me.btnAdjustngs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnAdjustngs.Location = New System.Drawing.Point(70, 64)
        Me.btnAdjustngs.Name = "btnAdjustngs"
        Me.btnAdjustngs.Size = New System.Drawing.Size(50, 28)
        Me.btnAdjustngs.TabIndex = 2
        Me.btnAdjustngs.TabStop = False
        Me.btnAdjustngs.Visible = False
        '
        'btnRegisterncs
        '
        Me.btnRegisterncs.BackColor = System.Drawing.Color.Yellow
        Me.btnRegisterncs.Location = New System.Drawing.Point(12, 12)
        Me.btnRegisterncs.Name = "btnRegisterncs"
        Me.btnRegisterncs.Size = New System.Drawing.Size(52, 33)
        Me.btnRegisterncs.TabIndex = 3
        Me.btnRegisterncs.TabStop = False
        Me.btnRegisterncs.Visible = False
        '
        'btnAdjustncs
        '
        Me.btnAdjustncs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnAdjustncs.Location = New System.Drawing.Point(70, 12)
        Me.btnAdjustncs.Name = "btnAdjustncs"
        Me.btnAdjustncs.Size = New System.Drawing.Size(36, 33)
        Me.btnAdjustncs.TabIndex = 4
        Me.btnAdjustncs.TabStop = False
        Me.btnAdjustncs.Visible = False
        '
        'btnBackDefectHome
        '
        Me.btnBackDefectHome.BackColor = System.Drawing.Color.Transparent
        Me.btnBackDefectHome.Location = New System.Drawing.Point(11, 508)
        Me.btnBackDefectHome.Name = "btnBackDefectHome"
        Me.btnBackDefectHome.Size = New System.Drawing.Size(201, 92)
        Me.btnBackDefectHome.TabIndex = 6
        Me.btnBackDefectHome.TabStop = False
        '
        'pbRefectData
        '
        Me.pbRefectData.BackColor = System.Drawing.Color.Transparent
        Me.pbRefectData.Location = New System.Drawing.Point(665, 27)
        Me.pbRefectData.Name = "pbRefectData"
        Me.pbRefectData.Size = New System.Drawing.Size(66, 83)
        Me.pbRefectData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbRefectData.TabIndex = 7
        Me.pbRefectData.TabStop = False
        '
        'defectHome
        '
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.pbRefectData)
        Me.Controls.Add(Me.btnBackDefectHome)
        Me.Controls.Add(Me.btnAdjustncs)
        Me.Controls.Add(Me.btnRegisterncs)
        Me.Controls.Add(Me.btnAdjustngs)
        Me.Controls.Add(Me.btnRegisterngs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "defectHome"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.btnRegisterngs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdjustngs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRegisterncs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdjustncs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBackDefectHome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbRefectData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btn_back As PictureBox
    Friend WithEvents btnAdjustnc As PictureBox
    Friend WithEvents btnRegisternc As PictureBox
    Friend WithEvents btnRegisterng As PictureBox
    Friend WithEvents btnAdjustng As PictureBox
    Friend WithEvents btnRegisterngs As PictureBox
    Friend WithEvents btnAdjustngs As PictureBox
    Friend WithEvents btnRegisterncs As PictureBox
    Friend WithEvents btnAdjustncs As PictureBox
    Friend WithEvents btnBackDefectHome As PictureBox
    Friend WithEvents pbRefectData As PictureBox
End Class
