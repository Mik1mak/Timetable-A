import { Component, OnInit } from '@angular/core';
import { UserService, GroupsService } from '@app/_services';

@Component({
  selector: 'app-shere',
  templateUrl: './shere.component.html'
})
export class ShereComponent implements OnInit {
  
  readShereLink?: string;
  editShereLink?: string;
  editMode: boolean;

  constructor(
    private authenticationService: UserService, 
    private groupService: GroupsService) {
      this.editMode = authenticationService.currentUserEditMode;
    }

  ngOnInit(): void {
    this.groupService.selected.subscribe(selectedGroups => {
      const firstPartUrl = `${window.location.protocol}//${window.location.host}/login/?id=${this.authenticationService.currentUserValue.id}&key=`;
      const encodedGroups = `&returnUrl=${this.encodeGroupIds(selectedGroups)}`;

      this.readShereLink = firstPartUrl + this.authenticationService.currentUserValue.readKey + encodedGroups;
      this.editShereLink = firstPartUrl + this.authenticationService.currentUserValue.editKey + encodedGroups;
    });
  }

  private encodeGroupIds(ids: number[]) {
    let output = '/?';
    
    for (let id of ids)
      output += `g=${id}&`;
    
    return encodeURIComponent(output.slice(0, -1));
  }
}
