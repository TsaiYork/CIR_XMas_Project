import { Injectable } from '@angular/core';
import { Player } from './player';
import { PLAYERS } from './mock-players';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private messageService: MessageService) { }

  getPlayer(id: number): Observable<Player> {
    this.messageService.add('PlayerService: fetched heroes');
    return of(PLAYERS.find(player => player.Id === id));
  }

  getPlayers(): Observable<Player[]> {
    this.messageService.add('PlayerService: fetched heroes');
    return of(PLAYERS);
  }
}
