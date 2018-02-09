Public Class Random

    Public Shared Function GetRandomInteger(Min As Integer, Max As Integer) As Integer

        Randomize()

        Dim randomNumber As Single = VBMath.Rnd()

        For i As Integer = 1 To Max - Min + 1

            If randomNumber < i / (Max - Min + 1) Then

                GetRandomInteger = i

                Exit For

            End If

        Next

    End Function

End Class
