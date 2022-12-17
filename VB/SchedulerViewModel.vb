Imports System
Imports System.Collections.ObjectModel
Imports System.Drawing

Namespace SchedulerBindToObservableCollectionWpf

    Public Class SchedulerViewModel

        Private _Appointments As ObservableCollection(Of SchedulerBindToObservableCollectionWpf.ModelAppointment), _Resources As ObservableCollection(Of SchedulerBindToObservableCollectionWpf.ModelResource)

        Public Property Appointments As ObservableCollection(Of ModelAppointment)
            Get
                Return _Appointments
            End Get

            Private Set(ByVal value As ObservableCollection(Of ModelAppointment))
                _Appointments = value
            End Set
        End Property

        Public Property Resources As ObservableCollection(Of ModelResource)
            Get
                Return _Resources
            End Get

            Private Set(ByVal value As ObservableCollection(Of ModelResource))
                _Resources = value
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
            Next
        End Sub

        Private Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String, ByVal ResColor As Integer, ByVal sortOrder As Integer) As ModelResource
            Dim mRes As ModelResource = New ModelResource()
            mRes.Id = res_id
            mRes.Name = caption
            mRes.Color = ResColor
            mRes.ResSortOrder = sortOrder
            Return mRes
        End Function

        Private Function CreateEvent(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As ModelAppointment
            Dim apt As ModelAppointment = New ModelAppointment()
            apt.Subject = subject
            apt.ResourceId = resourceId
            Dim rnd As Random = New Random()
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
            apt.Status = status
            apt.Label = label
            Return apt
        End Function

        Private Function ToRgb(ByVal color As System.Drawing.Color) As Integer
            Return color.B << 16 Or color.G << 8 Or color.R
        End Function
    End Class
End Namespace
