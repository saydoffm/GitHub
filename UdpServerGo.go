package main

import (
    "fmt"
    "net"
    "os"
    /*"time"*/
	"strings"
)

 

var allClientsTypeB map[*Client]int

type Client struct {

    outgoing   chan string
    addr		*net.UDPAddr
    conn       *net.UDPConn
    connection *Client
}


func NewClient(connection *net.UDPConn,addrin *net.UDPAddr) *Client {
	/*fmt.Println(addrin)*/
    client := &Client{
        outgoing: make(chan string),
        conn:     connection,
		addr:	addrin,
        
    }
	/*fmt.Println(client)*/
    return client
}

func main() {
    
	allClientsTypeB = make( map[*Client]int)
	udpAddrTypeA := net.UDPAddr{
        Port: 9050,}
	/*fmt.Println("udpAddrTypeA")*/
	udpAddrTypeB := net.UDPAddr{
        Port: 5090,}
	/*fmt.Println("udpAddrTypeB")*/
    connTypeA, err := net.ListenUDP("udp", &udpAddrTypeA)
	/*fmt.Println("connTypeA, err := net.ListenUDP(udp, &udpAddrTypeA)")*/
    checkError(err)
	connTypeB, errb := net.ListenUDP("udp", &udpAddrTypeB)
	/*fmt.Println("connTypeB, err := net.ListenUDP(udp, &udpAddrTypeB)")*/
    checkError(errb)
	go handleClientTypeB(connTypeB)
	/*fmt.Println("go handleClientTypeB(connTypeB)")*/
    for {
	/*fmt.Printf("data enter");*/
        handleClient(connTypeA)
		/*fmt.Printf("after handle");*/
		
    }

}

 func handleClientTypeB(conn *net.UDPConn){
  var buf [2048]byte
  /*fmt.Println("****************************************************************")*/
    line, addr, err := conn.ReadFromUDP(buf[0:])
    if err != nil {
        return
    }
	/*fmt.Println(addr)*/
	/*fmt.Println(string(buf[0:line]))*/
	
	if strings.Contains(string(buf[0:line]), "DISCONNECTED")	{
	/*fmt.Println("****************************************************************")*/
	/*fmt.Println("if string(buf[0:line]) ")*/
		client := NewClient(conn,addr)
		delete(allClientsTypeB,client)
		
		return
	}
	if strings.Contains(string(buf[0:line]) , "CONNECTED")	{
		/*fmt.Println("if string(buf[0:line]) ")*/
		client := NewClient(conn ,addr)
		/*fmt.Println("client := NewClient(conn ,addr)")*/
        for clientList, _ := range allClientsTypeB {
			/*fmt.Println(clientList)*/
            if clientList.connection == nil {
				/*fmt.Println("if clientList.connection == nil")*/
                client.connection = clientList
                clientList.connection = client
                /*fmt.Println("Connected")*/
            }
        }
		allClientsTypeB[client] = 1
		/*fmt.Println(len(allClientsTypeB))*/
	}
}
func  handleClient(conn *net.UDPConn)  {

	/*fmt.Println("handleClient")*/
    var buf [2048]byte
    _, addr, err := conn.ReadFromUDP(buf[0:])
	/*fmt.Println("_, addr, err := conn.ReadFromUDP(buf[0:])")*/
    if err != nil {
        return
    }
	datatosend := string(buf[0:2048])
    /*daytime := time.Now().String()*/
	fmt.Println(addr)
	/*fmt.Println(daytime)*/
	/*fmt.Printf("data rec");*/
	for clientList, _ := range allClientsTypeB {
			/*fmt.Println("Run on client list")*/
			
				/*fmt.Printf("Send to client ")*/
				/*fmt.Println(clientList.addr)*/
				clientList.conn.WriteToUDP([]byte(datatosend),clientList.addr)
			
		}
   
}

 

func checkError(err error) {

    if err != nil {
        fmt.Fprintf(os.Stderr, "Fatal error ", err.Error())
        os.Exit(1)
    }
}