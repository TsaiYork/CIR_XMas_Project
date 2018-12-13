import { Component, OnInit } from '@angular/core';
import { Player } from '../player';
import { ApiService } from '../api.service';
import { getPlayers } from '@angular/core/src/render3/players';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})

export class PlayersComponent implements OnInit {

  players: Player[];
  selectPlayer: Player;
  constructor(private apiService: ApiService) { }

  getPlayers(): void {
    this.apiService.getPlayers().subscribe(players => this.players = players);
  }

  ngOnInit() {
    this.getPlayers();
  }

  onSelect(player: Player): void {
    this.selectPlayer = player;
  }

}
