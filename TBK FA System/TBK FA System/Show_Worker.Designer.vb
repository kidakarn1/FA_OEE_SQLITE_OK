<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Show_Worker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_Worker))
        Me.Panel_show_worker = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.btn_up = New System.Windows.Forms.PictureBox()
        Me.btn_down = New System.Windows.Forms.PictureBox()
        Me.lbTotalWorker = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_up, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_down, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel_show_worker
        '
        Me.Panel_show_worker.AutoScroll = True
        Me.Panel_show_worker.Location = New System.Drawing.Point(28, 96)
        Me.Panel_show_worker.Name = "Panel_show_worker"
        Me.Panel_show_worker.Size = New System.Drawing.Size(646, 403)
        Me.Panel_show_worker.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(650, 96)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 403)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Location = New System.Drawing.Point(12, 516)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(193, 79)
        Me.btnBack.TabIndex = 1
        Me.btnBack.TabStop = False
        '
        'btn_up
        '
        Me.btn_up.BackColor = System.Drawing.Color.Transparent
        Me.btn_up.Location = New System.Drawing.Point(688, 90)
        Me.btn_up.Name = "btn_up"
        Me.btn_up.Size = New System.Drawing.Size(111, 208)
        Me.btn_up.TabIndex = 2
        Me.btn_up.TabStop = False
        '
        'btn_down
        '
        Me.btn_down.BackColor = System.Drawing.Color.Transparent
        Me.btn_down.Location = New System.Drawing.Point(682, 301)
        Me.btn_down.Name = "btn_down"
        Me.btn_down.Size = New System.Drawing.Size(111, 208)
        Me.btn_down.TabIndex = 3
        Me.btn_down.TabStop = False
        '
        'lbTotalWorker
        '
        Me.lbTotalWorker.AutoSize = True
        Me.lbTotalWorker.BackColor = System.Drawing.Color.Transparent
        Me.lbTotalWorker.Font = New System.Drawing.Font("Catamaran", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalWorker.ForeColor = System.Drawing.Color.White
        Me.lbTotalWorker.Location = New System.Drawing.Point(753, 23)
        Me.lbTotalWorker.Name = "lbTotalWorker"
        Me.lbTotalWorker.Size = New System.Drawing.Size(28, 17)
        Me.lbTotalWorker.TabIndex = 4
        Me.lbTotalWorker.Text = "XX"
        '
        'Show_Worker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.lbTotalWorker)
        Me.Controls.Add(Me.btn_down)
        Me.Controls.Add(Me.btn_up)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel_show_worker)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Show_Worker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_up, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_down, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel_show_worker As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnBack As PictureBox
    Friend WithEvents btn_up As PictureBox
    Friend WithEvents btn_down As PictureBox
    Friend WithEvents lbTotalWorker As Label
End Class
