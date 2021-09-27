using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap_mat : MonoBehaviour
{
    public Material mat_good;
    public Material mat_bad;
    public Material mat_sick;

    public PersonState current_state;
    public MeshRenderer meshRenderer;

    public void DoAction(Actions action)
    {
        if (current_state.links.ContainsKey(action))
        {
            current_state = current_state.links[action];
            current_state.Set();
        }
    }

    public class PersonState
    {
        public Material mat;
        public Dictionary<Actions, PersonState> links;
        public MeshRenderer mesh;

        public PersonState(Material mat, MeshRenderer mesh)
        {
            this.mat = mat;
            this.mesh = mesh;
            links = new Dictionary<Actions, PersonState>();
        }

        public void Set()
        {
            mesh.material = mat;
        }
    }

    public enum Actions
    {
        LEFT, RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        PersonState state_sick = new PersonState(mat_sick, meshRenderer);
        PersonState state_good = new PersonState(mat_good, meshRenderer);
        PersonState state_bad = new PersonState(mat_bad, meshRenderer);

        state_sick.links.Add(Actions.LEFT, state_bad);
        state_sick.links.Add(Actions.RIGHT, state_good);
        state_good.links.Add(Actions.LEFT, state_sick);
        state_bad.links.Add(Actions.RIGHT, state_sick);

        current_state = state_sick;
        current_state.Set();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
