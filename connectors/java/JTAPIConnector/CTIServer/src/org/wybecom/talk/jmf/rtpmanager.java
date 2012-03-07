/*
 *  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 *  Copyright (C) 2010 WYBECOM
 *
 *  Yohann BARRE <y.barre@wybecom.com>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  T.A.L.K. is based upon:
 *  - Sun JTAPI http://java.sun.com/products/jtapi/
 *  - JulMar TAPI http://julmar.com/
 *  - Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 */

/**
 * \file
 * Implementation of an RTP Manager
 * \package
 * All JMF related libraries 
 */
package org.wybecom.talk.jmf;

import java.net.*;
import java.util.*;
import javax.media.rtp.*;
import javax.media.*;
import javax.media.control.*;
import javax.media.format.*;
import javax.media.protocol.*;
import java.io.*;
/**
 * 
 * @author Yohann BARRE
 */
public class rtpmanager {

    SessionAddress localAddress;
    RTPManager manager;
    MediaLocator medialocator;
    DataSource ds;
    Processor p;
    Boolean processorok;
    SendStream sendStream;

    public rtpmanager() throws MalformedURLException, java.lang.InstantiationException, InvalidSessionAddressException, javax.media.NoDataSourceException, java.lang.IllegalAccessException, NoProcessorException, IOException, java.lang.InterruptedException, javax.media.UnsupportedPlugInException{
        manager = RTPManager.newInstance();
        localAddress = new SessionAddress();
        File file = new File("C:/attente_callcenter_reunica.wav");
        medialocator = new MediaLocator(file.toURL());
        processorok = createProcessor();
        manager.initialize(localAddress);
    }

    public synchronized boolean createProcessor() throws IOException, NoDataSourceException, NoProcessorException, java.lang.InterruptedException, javax.media.UnsupportedPlugInException {
        ds = Manager.createDataSource(medialocator);
        System.out.println( "DataSource created from file" ) ;
        p = Manager.createProcessor(ds);
        System.out.println( "Processor created" ) ;
        p.addControllerListener(new StateListener());

        int state = Processor.Configured;
        if (state == Processor.Configured){
            p.configure();
            System.out.println( "Processor Configuring..." ) ;
        }

        while ( p.getState() != Processor.Configured )
            {
                System.out.println( "Processor not yet configured..." ) ;
                Thread.sleep(10);
            }

            TrackControl [] track = p.getTrackControls() ;
            boolean encoding = false;

            if ( ( track == null ) || ( track.length < 1 ) )
            {
                System.out.println( "Error: No tracks in Source" ) ;
            }
            else {
                System.out.println(String.valueOf(track.length) + " tracks founded");
                ContentDescriptor content = new ContentDescriptor( ContentDescriptor.RAW_RTP ) ;
                p.setContentDescriptor( content ) ;

                Format supportedFormats[] ;

                for ( int i = 0 ; i < track.length ; i++ )
                {
                    // Set format to the format of TrackControl
                    // i.e. ContentDescriptor
                    Format format = track[i].getFormat() ;
                    if ( track[i].isEnabled() )
                    {
                        // Find formats that support RAW_RTP
                        supportedFormats = track[i].getSupportedFormats() ;

                        if ( supportedFormats.length > 0 )
                        {
                            System.out.println( String.valueOf(supportedFormats.length) + " supported format: " + supportedFormats[0].toString());
                            // Encode the track with MPEG_RTP format
                            if( track[i].setFormat( supportedFormats[i] ) == null )
                            {
                                track[i].setEnabled( false ) ;
                                encoding = false ;
                            } else {
                                track[i].setEnabled( true ) ;
                                Codec codec[] = new Codec[3];
                                codec[0] = new com.ibm.media.codec.audio.rc.RCModule();
                                codec[1] = new com.ibm.media.codec.audio.ulaw.JavaEncoder();
                                codec[2] = new com.sun.media.codec.audio.ulaw.Packetizer();
                                ((com.sun.media.codec.audio.ulaw.Packetizer)codec[2]).setPacketSize(160);
                                ((TrackControl)track[i]).setCodecChain(codec);
                                encoding = true ;
                            }
                        }
                    }
                }

                int realized = Controller.Realized;
                if (encoding){
                    if (realized == Processor.Realized){
                        p.realize();
                        System.out.println( "Processor Realizing" ) ;
                    }
                }

                while ( p.getState() != Processor.Realized )
                    {
                        System.out.println( "Processor not yet realized..." ) ;
                        Thread.sleep(10);
                    }

                return true;
            }
            return false;
    }

    public int getPort(){
        return localAddress.getDataPort();
    }

    public void addTarget  (String address, int port) throws UnknownHostException, InvalidSessionAddressException, IOException {
        InetAddress remote = InetAddress.getByName(address);
        manager.addTarget(new SessionAddress(remote,port));
    }

    public void start() throws javax.media.format.UnsupportedFormatException, IOException {
        System.out.println("Starting processor...");
        p.start();
        ds = p.getDataOutput();
        sendStream = manager.createSendStream(ds, 0);
        System.out.println("Starting stream: " + sendStream.getParticipant().getCNAME());
        sendStream.start();
    }

    public void stop(){
        p.stop();
        p.setMediaTime(new Time(0));
        manager.removeTargets("Session done");
    }

    class StateListener implements ControllerListener
        {
        public void controllerUpdate( ControllerEvent c )
        {
            System.out.println(c.toString());
            if( c instanceof ControllerClosedEvent )
            {   if( p != null ){
                p.stop() ;
                p.close() ;
                p = null ;
                manager.removeTargets( "Sessions are done" ) ;
                manager.dispose() ;
                }
            }
            if( c instanceof EndOfMediaEvent )
            {
                System.out.println( "End of Media Stream, restarting..." ) ;
                p.setMediaTime(new Time(0));
                p.start();
            }
        }
        }

}
