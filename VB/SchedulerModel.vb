Imports Microsoft.VisualBasic
Imports System

Namespace SchedulerBindToObservableCollectionWpf
#Region "#models    "
	Public Class ModelAppointment
		Private privateStartTime As DateTime
		Public Property StartTime() As DateTime
			Get
				Return privateStartTime
			End Get
			Set(ByVal value As DateTime)
				privateStartTime = value
			End Set
		End Property
		Private privateEndTime As DateTime
		Public Property EndTime() As DateTime
			Get
				Return privateEndTime
			End Get
			Set(ByVal value As DateTime)
				privateEndTime = value
			End Set
		End Property
		Private privateSubject As String
		Public Property Subject() As String
			Get
				Return privateSubject
			End Get
			Set(ByVal value As String)
				privateSubject = value
			End Set
		End Property
		Private privateStatus As Integer
		Public Property Status() As Integer
			Get
				Return privateStatus
			End Get
			Set(ByVal value As Integer)
				privateStatus = value
			End Set
		End Property
		Private privateDescription As String
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property
		Private privateLabel As Long
		Public Property Label() As Long
			Get
				Return privateLabel
			End Get
			Set(ByVal value As Long)
				privateLabel = value
			End Set
		End Property
		Private privateLocation As String
		Public Property Location() As String
			Get
				Return privateLocation
			End Get
			Set(ByVal value As String)
				privateLocation = value
			End Set
		End Property
		Private privateAllDay As Boolean
		Public Property AllDay() As Boolean
			Get
				Return privateAllDay
			End Get
			Set(ByVal value As Boolean)
				privateAllDay = value
			End Set
		End Property
		Private privateEventType As Integer
		Public Property EventType() As Integer
			Get
				Return privateEventType
			End Get
			Set(ByVal value As Integer)
				privateEventType = value
			End Set
		End Property
		Private privateRecurrenceInfo As String
		Public Property RecurrenceInfo() As String
			Get
				Return privateRecurrenceInfo
			End Get
			Set(ByVal value As String)
				privateRecurrenceInfo = value
			End Set
		End Property
		Private privateReminderInfo As String
		Public Property ReminderInfo() As String
			Get
				Return privateReminderInfo
			End Get
			Set(ByVal value As String)
				privateReminderInfo = value
			End Set
		End Property
		Private privateResourceId As Object
		Public Property ResourceId() As Object
			Get
				Return privateResourceId
			End Get
			Set(ByVal value As Object)
				privateResourceId = value
			End Set
		End Property
		Private privatePrice As Decimal
		Public Property Price() As Decimal
			Get
				Return privatePrice
			End Get
			Set(ByVal value As Decimal)
				privatePrice = value
			End Set
		End Property

		Public Sub New()
		End Sub
	End Class

	Public Class ModelResource
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateColor As Integer
		Public Property Color() As Integer
			Get
				Return privateColor
			End Get
			Set(ByVal value As Integer)
				privateColor = value
			End Set
		End Property
		Private privateResSortOrder As Integer
		Public Property ResSortOrder() As Integer
			Get
				Return privateResSortOrder
			End Get
			Set(ByVal value As Integer)
				privateResSortOrder = value
			End Set
		End Property

		Public Sub New()
		End Sub
	End Class
#End Region ' #models
End Namespace
