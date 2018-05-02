namespace CIS194

module Log =

    type MessageType = 
        Info
        | Warning
        | Error of int

    type TimeStamp = Int

    type LogMessage = 
        LogMessage of MessageType * TimeStamp * string
        | Unknown of string
