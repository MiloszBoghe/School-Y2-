package be.pxl.webcomponenten;

import javax.ws.rs.*;

@Path("/hello")
public class HelloRest {

    @Path("/{name}")
    @GET
    //@Produces("Application/Json") is niet correct
    public String sayHello(@PathParam("name")String name, @QueryParam("lang") String lang){
        return "Hello "+name+", the language is: "+lang;
    }

    @PUT
    public void putHello(){
        System.out.println("put request!");
    }

    @POST
    public void postHello(){
        System.out.println("post request!");
    }

    @DELETE
    public void deleteHello(){
        System.out.println("delete request!");
    }
}
