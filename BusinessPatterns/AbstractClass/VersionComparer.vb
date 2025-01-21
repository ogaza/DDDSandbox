Imports System.Collections.Generic
Imports System.Linq

Public Class VersionNumberComparer

  Dim lMajorNumber As Integer = 0
  Dim lMinorNumber As Integer = 0
  Dim lRevisionNumber As Integer = 0
  Dim lInternalRevisionNmbr As Integer = 0

  ''' <summary>
  ''' My implementation - Olaf Gaza
  ''' </summary>
  ''' <param name="version"></param>
  ''' <returns></returns>
  Public Function IsVersionNumberBigger_v2(ByVal version As String) As Boolean

    Dim tmp_versionNumbers() As Integer = version.Split(".").Select(Function(x) CInt(x)).ToArray()

    Dim versionNumbers() As Integer = {lMajorNumber, lMinorNumber, lRevisionNumber, lInternalRevisionNmbr}

    For i As Integer = 0 To tmp_versionNumbers.Length
      If tmp_versionNumbers(i) > versionNumbers(i) Then
        Return True
      End If
    Next

    For i As Integer = 0 To tmp_versionNumbers.Length
      If tmp_versionNumbers(i) < versionNumbers(i) Then
        Return False
      End If
    Next

    Return False

  End Function

  ''' <summary>
  ''' Copied from TechServices
  ''' </summary>
  ''' <param name="version_1"></param>
  ''' <param name="version_2"></param>
  ''' <returns></returns>
  Public Function IsVersionNumberBigger(ByVal version_1 As String, ByVal version_2 As String) As Boolean

    Dim tmp_szVersionNumbers() As String = version_1.Split(".")
    Dim tmp_lMajorNumber As Integer = CInt(tmp_szVersionNumbers(0))
    Dim tmp_lMinorNumber As Integer = CInt(tmp_szVersionNumbers(1))
    Dim tmp_lRevisionNumber As Integer = CInt(tmp_szVersionNumbers(2))
    Dim tmp_lInternalRevisionNmbr As Integer = CInt(tmp_szVersionNumbers(3))

    Dim tmp2_szVersionNumbers() As String = version_2.Split(".")
    Dim tmp2_lMajorNumber As Integer = CInt(tmp2_szVersionNumbers(0))
    Dim tmp2_lMinorNumber As Integer = CInt(tmp2_szVersionNumbers(1))
    Dim tmp2_lRevisionNumber As Integer = CInt(tmp2_szVersionNumbers(2))
    Dim tmp2_lInternalRevisionNmbr As Integer = CInt(tmp2_szVersionNumbers(3))

    If tmp_lMajorNumber > tmp2_lMajorNumber Then
      Return True
    End If
    If tmp_lMinorNumber > tmp2_lMinorNumber And tmp_lMajorNumber >= tmp2_lMajorNumber Then
      Return True
    End If
    If tmp_lRevisionNumber > tmp2_lRevisionNumber And tmp_lMinorNumber >= tmp2_lMinorNumber And tmp_lMajorNumber >= tmp2_lMajorNumber Then
      Return True
    End If
    If tmp_lInternalRevisionNmbr > tmp2_lInternalRevisionNmbr And tmp_lRevisionNumber >= tmp2_lRevisionNumber And tmp_lMinorNumber >= tmp2_lMinorNumber And tmp_lMajorNumber >= tmp2_lMajorNumber Then
      Return True
    End If
    Return False
  End Function

  '''
  ' Combines 2 lists of version numbers into an sorted list
  ' Example:  list_1  "35.0.0.2, 35.0.0.23, 35.0.0.37"
  '           list_2  "35.0.0.4, 35.0.0.55"
  '           result  "35.0.0.2, 35.0.0.4, 35.0.0.23, 35.0.0.37, 35.0.0.55"
  Public Function GetCombinedVersion(
                                     colVersions As List(Of String),
                                     colBranchVersions As List(Of String)) As List(Of String)
    Dim combined As List(Of String) = New List(Of String)
    Dim max As Integer = colVersions.Count + colBranchVersions.Count
    Dim iCommon As Integer = 0
    Dim iBranch As Integer = 0
    While max > 0
      If iCommon >= colVersions.Count Then
        If iBranch < colBranchVersions.Count Then
          combined.Add(colBranchVersions.Item(iBranch))
          iBranch += 1
        End If
        max -= 1
        Continue While
      End If
      If iBranch >= colBranchVersions.Count Then
        If iCommon < colVersions.Count Then
          combined.Add(colVersions.Item(iCommon))
          iCommon += 1
        End If
        max -= 1
        Continue While
      End If
      If IsVersionNumberBigger(colVersions.Item(iCommon), colBranchVersions.Item(iBranch)) Then
        combined.Add(colBranchVersions.Item(iBranch))
        iBranch += 1
        max -= 1
        Continue While
      End If
      If IsVersionNumberBigger(colBranchVersions.Item(iBranch), colVersions.Item(iCommon)) Then
        combined.Add(colVersions.Item(iCommon))
        iCommon += 1
        max -= 1
        Continue While
      End If
      If colBranchVersions.Item(iBranch) = colVersions.Item(iCommon) Then
        combined.Add(colVersions.Item(iCommon))
        iCommon += 1
        iBranch += 1
        max -= 1
        Continue While
      End If
    End While
    Return combined
  End Function

End Class
