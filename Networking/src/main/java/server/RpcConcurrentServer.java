package server;

import rpc.ClientRpcWorker;
import services.IServices;

import java.net.Socket;

public class RpcConcurrentServer extends AbsConcurrentServer {
    private final IServices server;

    public RpcConcurrentServer(int port, IServices server) {
        super(port);
        this.server = server;
        System.out.println("RpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        ClientRpcWorker worker = new ClientRpcWorker(server, client);

        return new Thread(worker);
    }
}
