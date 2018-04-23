Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports System.Collections.ObjectModel
Imports System.Drawing

Namespace SchedulerBindToObservableCollectionWpf
	Public Class SchedulerViewModel
		Private privateAppointments As ObservableCollection(Of ModelAppointment)
		Public Property Appointments() As ObservableCollection(Of ModelAppointment)
			Get
				Return privateAppointments
			End Get
			Private Set(ByVal value As ObservableCollection(Of ModelAppointment))
				privateAppointments = value
			End Set
		End Property
		Private privateResources As ObservableCollection(Of ModelResource)
		Public Property Resources() As ObservableCollection(Of ModelResource)
			Get
				Return privateResources
			End Get
			Private Set(ByVal value As ObservableCollection(Of ModelResource))
				privateResources = value
			End Set
		End Property

		Public Sub New()
			Appointments = New ObservableCollection(Of ModelAppointment)()
			Resources = New ObservableCollection(Of ModelResource)()
			AddTestData()
		End Sub

		Private Sub AddTestData()
			Resources.Add(CreateCustomResource(1, "Max Fowler", ToRgb(Color.PowderBlue), 3))
			Resources.Add(CreateCustomResource(2, "Nancy Drewmore", ToRgb(Color.PaleVioletRed), 1))
			Resources.Add(CreateCustomResource(3, "Pak Jang", ToRgb(Color.PeachPuff), 2))

			For Each resource As ModelResource In Resources
				Dim subjPrefix As String = resource.Name & "'s "
				Appointments.Add(CreateEvent(subjPrefix & "meeting", resource.Id, 2, 5))
				Appointments.Add(CreateEvent(subjPrefix & "travel", resource.Id, 3, 6))
				Appointments.Add(CreateEvent(subjPrefix & "phone call", resource.Id, 0, 10))
			Next resource
		End Sub

		Private Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String, ByVal ResColor As Integer, ByVal sortOrder As Integer) As ModelResource
			Dim mRes As New ModelResource()
			mRes.Id = res_id
			mRes.Name = caption
			mRes.Color = ResColor
			mRes.ResSortOrder = sortOrder
			Return mRes
		End Function

		Private Function CreateEvent(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As ModelAppointment
			Dim apt As New ModelAppointment()
			apt.Subject = subject
			apt.ResourceId = resourceId
			Dim rnd As New Random()

			Dim rangeInMinutes As Integer = 60 * 24
			apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
			apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
			apt.Status = status
			apt.Label = label
			Return apt
		End Function

		Private Function ToRgb(ByVal color As System.Drawing.Color) As Integer
            Return Convert.ToInt32(color.B) << 16 Or Convert.ToInt32(color.G) << 8 Or Convert.ToInt32(color.R)
		End Function
	End Class
End Namespace