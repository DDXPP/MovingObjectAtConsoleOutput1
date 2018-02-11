Public Class Random

    Public Shared Function GetRandom(Min As Integer, Max As Integer) As Integer

        Randomize()

        Dim randomNumber As Single = VBMath.Rnd()

        For i As Integer = 1 To Max - Min + 1

            If randomNumber < i / (Max - Min + 1) Then

                Return i - 1

            End If

        Next

    End Function

    Public Shared Function GetRandom() As Double

        Randomize()

        Return VBMath.Rnd()

    End Function

End Class
