import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { ApiService } from '../api.service';
import { Player } from '../player';

@Component({
  selector: 'app-player-detail',
  templateUrl: './player-detail.component.html',
  styleUrls: ['./player-detail.component.css']
})
export class PlayerDetailComponent implements OnInit, OnDestroy {

  @Input() player: Player;

  constructor(
    private route: ActivatedRoute,
    private heroService: ApiService,
    private location: Location) { }

  ngOnInit() {
    this.getPlayer();
  }

  getPlayer(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.heroService.getPlayer(id)
      .subscribe(player => this.player = player);
  }

  ngOnDestroy() {
    console.log('ngOnDestroy');
}

}
